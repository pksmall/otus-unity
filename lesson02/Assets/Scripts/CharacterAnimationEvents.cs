using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void AttackEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }
    void HandAttackEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }

    void StateToDied()
    {
        var target = character.target.transform.GetComponent<Character>();
        if (target.name != character.name)
        {
            target.DoDamgeToTarget(character.GetDamage());
        }
    }

    void ShootEnd()
    {
        character.SetState(Character.State.Idle);
    }
}
