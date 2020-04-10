using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Velocity;
    private Transform Target;
    [SerializeField] private float _lifeTime = 5;

    public void Init(Vector3 from, Transform target)
    {
        Target = target;
        transform.position = from;
        // выстрел по физике
        GetComponent<Rigidbody2D>().AddForce((target.position - from).normalized * Velocity);
        Destroy(gameObject, _lifeTime);
    }

    // погоня за целью с равной скоростью
    //void Update()
    //{
    //    transform.position += (Target.position - transform.position).normalized
    //        * Velocity * Time.deltaTime;
    //}
}
