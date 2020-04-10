using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public float speed;
    public Transform startPos;

    public Vector3 nextPos;
    public float dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = 1.0f;
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            if (dir < 0.0f)
            {
                dir = 1.0f;
            }
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            if (dir < 0.0f)
            {
                nextPos = pos1.position;
            }
            else
            {
                nextPos = pos3.position;
            }
        }
        if (transform.position == pos3.position)
        {
            nextPos = pos2.position;
            dir = -1.0f;
        }
    
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
        Gizmos.DrawLine(pos2.position, pos3.position);
    }
}
