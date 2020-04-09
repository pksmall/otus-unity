using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        Died
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Knife,
        Hand
    }

    public float runSpeed;
    public float distanceFromEnemy;
    public Character target;
    public Weapon weapon;
    public Transform gunBarrel;
    public HealthBar healthBar;
    public State state = State.Idle;
    public Health currentHealth;
    public float damage;

    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Shot shot;
    CapsuleCollider capsuleCollider;
    ParticleSystem particleSystem;
    bool isAvailable = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        shot = FindObjectOfType<Shot>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        currentHealth = GetComponent<Health>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(currentHealth.current);
        StartCoroutine(CharacterLoop());
    }

    public void AttackEnemy()
    {
        if (target.name == this.name)
        {
            return;
        }
        if (target.state != State.Died && this.state != State.Died)
        {
            switch (weapon)
            {
                case Weapon.Hand:
                case Weapon.Knife:
                case Weapon.Bat:
                    state = State.RunningToEnemy;
                    break;

                case Weapon.Pistol:
                    state = State.BeginShoot;
                    break;
            }
        }
    }

/*    [ContextMenu("Weapon/Hand")]
    void WeaponHand()
    {
        weapon = Weapon.Hand;
        Debug.Log("I hava only my hand!");
    }
    [ContextMenu("Weapon/Bat")]
    void WeaponBat()
    {
        weapon = Weapon.Bat;
        Debug.Log("I hava a bat!");
    }
    [ContextMenu("Weapon/Knife")]
    void WeaponKnife()
    {
        weapon = Weapon.Knife;
        Debug.Log("I hava a knife!");
    }
    [ContextMenu("Weapon/Pistol")]
    void WeaponPistol()
    {
        weapon = Weapon.Pistol;
        Debug.Log("I hava a gun!");
    }
*/
    public float GetDamage()
    {
        return damage;
    }

    public bool IsIdle()
    {
        return state == State.Idle;
    }

    public bool IsDead()
    {
        return state == State.Died;
    }
    public void SetState(State newState)
    {
        state = newState;
    }


    bool IsAvailabel()
    {
        return isAvailable;
    }
    IEnumerator CharacterLoop()
    {
        while(IsAvailabel())
        {
            switch (state)
            {
                case State.Idle:
                    animator.SetFloat("speed", 0.0f);
                    transform.rotation = originalRotation;
                    break;

                case State.RunningToEnemy:
                    if (target.state != State.Died)
                    {
                        animator.SetFloat("speed", runSpeed);
                        while(!RunTowards(target.transform.position, distanceFromEnemy))
                        {
                            yield return null;
                        }
                        state = State.BeginAttack;
                    }
                    break;

                case State.RunningFromEnemy:
                    animator.SetFloat("speed", runSpeed);
                    while (!RunTowards(originalPosition, 0.0f))
                    {
                        yield return null;
                    }
                    state = State.Idle;
                    break;

                case State.BeginAttack:
                    if (target.state != State.Died)
                    {
                        animator.SetFloat("speed", 0.0f);
                        switch (weapon)
                        {
                            case Weapon.Bat:
                            case Weapon.Knife:
                                animator.SetTrigger("attack");
                                break;
                            case Weapon.Hand:
                                animator.SetTrigger("handAttack");
                                break;
                        }
                        state = State.Attack;
                    }
                    break;

                case State.Attack:
                    animator.SetFloat("speed", 0.0f);
                    break;

                case State.BeginShoot:
                    animator.SetFloat("speed", 0.0f);
                    animator.SetTrigger("shoot");
                    state = State.Shoot;
                    break;

                case State.Shoot:
                    if (target.state != State.Died)
                    {
                        animator.SetFloat("speed", 0.0f);
                        if (RotateToEnemy(target.transform.position, distanceFromEnemy))
                        {
                            var from = gunBarrel.position;
                            var to = new Vector3(target.transform.position.x, from.y, target.transform.position.z);

                            var direction = (to - from).normalized;
                            RaycastHit hit;
                            if (Physics.Raycast(from, direction, out hit, 100))
                            {
                                to = new Vector3(hit.point.x, from.y, hit.point.z);
                            }
                            else
                            {
                                to = from + direction * 100;
                            }
                            shot.Show(from, to);
                        }
                    }
                    break;

                case State.Died:
                    isAvailable = false;
                    break;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        return;
    }

    bool RotateToEnemy(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        return true;
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 vector = direction * runSpeed * Time.deltaTime;
        if (vector.magnitude < distance.magnitude) {
            transform.position += vector;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    public void kill()
    {
        if (state != State.Died)
        {
            particleSystem.Play();
            Destroy(capsuleCollider);
            animator.SetTrigger("died");
            state = State.Died;
            healthBar.SetHealth(0);
            healthBar.gameObject.SetActive(false);
        }
    }

    public void DoDamgeToTarget(float damage)
    {
        Health health = currentHealth;

        if (health != null)
        {
            health.ApplyDamage(damage);
            if (health.current <= 0.0f)
            {
                kill();
                return;
            }
            particleSystem.Play();
            healthBar.SetHealth(health.current);
        }
    }
}
