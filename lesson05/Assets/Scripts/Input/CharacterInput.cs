using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterInput : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Character _character;
    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<Character>();
        _character.MoveForce = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            _character.MoveLeft();
        } else if (Input.GetKey(KeyCode.D))
        {
            _character.MoveRight();
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            _character.Jump();
        }
    }
}
