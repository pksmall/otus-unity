using UnityEngine;
using UnityEngine.UI;
 
public class StartMenuPage : MonoBehaviour
{
    private const string BATTLE_SCENE_NAME = "Level#1";
    public string level1 = BATTLE_SCENE_NAME;
    public string level2 = "Level#2";
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    public LoadingLogic loadingLogic;
    public Button level1Button;
    public Button level2Button;
    public GameObject visualChoosePart;
    public GameObject settingRoot;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayGame);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(ExitGame);

        // choose level
        level1Button.onClick.AddListener(Level1Game);
        level2Button.onClick.AddListener(Level2Game);
    }

    private void Level1Game()
    {
        loadingLogic.LoadScene(level1);
    }

    private void Level2Game()
    {
        loadingLogic.LoadScene(level2);
    }

    private void PlayGame()
    {
        visualChoosePart.SetActive(true);
    }

    private void Settings()
    {
        settingRoot.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
