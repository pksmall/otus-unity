using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public Character[] playerCharacters;
    public Character[] enemyCharacters;
    Character currentTarget;
    bool waitinPlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    [ContextMenu("Player Move")]
    void PlayerMove()
    {
        if (waitinPlayerInput)
        {
            waitinPlayerInput = false;
        }
    }

    [ContextMenu("Switch character")]
    void SwitchCharacter()
    {
        for (int i = 0; i < enemyCharacters.Length; i++)
        {
            if (enemyCharacters[i] == currentTarget)
            {
                int start = i;
                i++;

                for (; i < enemyCharacters.Length; i++)
                {
                    if (enemyCharacters[i].IsDead())
                    {
                        continue;
                    }
                    currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(false);
                    currentTarget = enemyCharacters[i];
                    currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(true); 

                    return;
                 }
                for (i = 0; i < start; i++)
                {
                    if (enemyCharacters[i].IsDead())
                    {
                        continue;
                    }
                    currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(false);
                    currentTarget = enemyCharacters[i];
                    currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(true);

                    return;
                }
            }
        }

    }

    void PlayerWon() { Debug.Log("Player won!"); }
    void PlayerLost() { Debug.Log("Player lost!"); }

    Character FirstAliveCharacter(Character[] characters)
    {
         foreach(var character in characters)
        {
            if (!character.IsDead())
            {
                return character;
            }
        }
        return null;
    }
    bool CheckEndGame()
    {
        if (FirstAliveCharacter(playerCharacters) == null)
        {
            PlayerLost();
            return true;
        }

        if (FirstAliveCharacter(enemyCharacters) == null)
        {
            PlayerWon();
            return true;
        }

        return false;
    }

    IEnumerator GameLoop()
    {
        while (!CheckEndGame())
        {
            foreach (var player in playerCharacters)
            {
                if (player.IsDead()) { continue; }

                Character target = FirstAliveCharacter(enemyCharacters);
                if (target == null) { break; }

                currentTarget = target;
                currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(true);

                waitinPlayerInput = true;
                while (waitinPlayerInput) { yield return null; }

                currentTarget.GetComponentInChildren<TargetIndicator>(true).gameObject.SetActive(false);
                
                player.target = currentTarget;
                player.AttackEnemy();
                while (!player.IsIdle()) { yield return null; }
            }

            foreach (var enemy in enemyCharacters)
            {
                if (enemy.IsDead()) { continue; }

                Character target = FirstAliveCharacter(playerCharacters);
                if (target == null) { break; }

                enemy.target = target;

                enemy.AttackEnemy();
                while (!enemy.IsIdle()) { yield return null; }
            }
        }
    }
}
