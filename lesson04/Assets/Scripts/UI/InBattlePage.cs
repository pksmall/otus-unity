using UnityEngine;
using UnityEngine.UI;

class InBattlePage : MonoBehaviour
{
    public GameContoller contoller;
    public PauseMenuLogic pauseMenuLogic;

    public Button attackButton;
    public Button switchButton;
    public Button pauseButton;

    private void Awake()
    {
        attackButton.onClick.AddListener(OnAttackButtonClick);
        switchButton.onClick.AddListener(() => contoller.SwitchCharacter());
        pauseButton.onClick.AddListener(() => pauseMenuLogic.OnPauseButtonClick());
    }

    private void OnAttackButtonClick()
    {
        contoller.PlayerMove();
    }
}
