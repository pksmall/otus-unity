using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player I;

    private void Awake()
    {
        I = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Character>() == null)
            return;
        var character = collision.transform.GetComponent<Character>();
        GameOver(character);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Projectile>() == null)
            return;
        var character = collider.transform.GetComponent<Character>();
        GameOver(character);
    }

    public static void GameOver(Character character)
    {
        Time.timeScale = 0.0f;
        //character.Died();
        Debug.Log("Game over");
    }
}
