using UnityEngine;

public class EnemyTudaSudaController : Character
{
    public Collider2D Platform;
    float PlatformWidth;
    bool direction;

    protected override void Start()
    {
        base.Start();
        PlatformWidth = Platform.bounds.size.x;
    }

    protected override void Update()
    {
        base.Update();

        var position = Platform.transform.position.x;
        var left = position - PlatformWidth / 3;
        var right = position + PlatformWidth / 3;
        
        if (transform.position.x < left)
            direction = true;
        else if (transform.position.x > right)
            direction = false;
        if (direction) MoveRight();
        else MoveLeft();
    }
}