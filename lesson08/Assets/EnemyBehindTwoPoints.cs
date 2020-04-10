using UnityEngine;

public class EnemyBehindTwoPoints : MonoBehaviour
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1.position, pos2.position, Mathf.PingPong(Time.time * speed, 1.0f));
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
