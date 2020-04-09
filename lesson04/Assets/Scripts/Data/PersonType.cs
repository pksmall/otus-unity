using UnityEngine;

[CreateAssetMenu(fileName = "PersonType", menuName = "PersonType", order = 10)]
class PersonType : ScriptableObject
{
    [Header("Жизнь персонажа, стандартная")]
    public int Health;
    [Space]
    [Header("Урон наносимый оружием персонажа по умолчанию")]
    [Range(1, 100)]
    [Tooltip("Не забуваємо ставити скільки треба.")]
    public float damage;
}
