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
    public Transform target;
    public Weapon weapon;
    public Transform gunBarrel;
    public State state = State.Idle;

    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Shot shot;
    CapsuleCollider capsuleCollider;
    Character enemy;
    ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        shot = FindObjectOfType<Shot>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        enemy = target.transform.GetComponent<Character>();
        if (enemy.name == this.name)
        {
            return;
        }
        if (enemy.state != State.Died && this.state != State.Died)
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

    [ContextMenu("Weapon/Hand")]
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

    public void SetState(State newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state) {
            case State.Idle:
                animator.SetFloat("speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                if (enemy.state != State.Died)
                {
                    animator.SetFloat("speed", runSpeed);
                    if (RunTowards(target.position, distanceFromEnemy))
                        state = State.BeginAttack;
                }
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginAttack:
                if (enemy.state != State.Died)
                {
                    animator.SetFloat("speed", 0.0f);
                    switch(weapon)
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
                if (enemy.state != State.Died)
                {
                    animator.SetFloat("speed", 0.0f);
                    if (RotateToEnemy(target.position, distanceFromEnemy))
                    {
                        var from = gunBarrel.position;
                        var to = new Vector3(target.position.x, from.y, target.position.z);

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
                break;
        }
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

        Vector3 vector = direction * runSpeed;
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
        }
    }
}
