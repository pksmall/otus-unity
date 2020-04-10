using UnityEngine;

public class EnemyZaGeroemController : Character
{
    float reload = 1;
    float reloadCurrent;
    public Projectile ProjectilePrefab;

    protected override void Update()
    {
        base.Update();
        var direction = transform.position.x < Player.I.transform.position.x;
        if (direction) MoveRight();
        else MoveLeft();

        if (reloadCurrent < 0)
            Fire();
        else reloadCurrent -= Time.deltaTime;
    }

    void Fire()
    {
        reloadCurrent = reload;
        var p = Instantiate(ProjectilePrefab);
        p.Init(transform.position, Player.I.transform);
    }
}
