using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerWonLogic : MonoBehaviour
{
    public GameObject visualBattlePart;
    public GameObject visualWonPart;
    public float fakeLoadTime = 0f;
    public Button loadAgainButton;
    public Button exitButton;

    const string PLAYER_WON = "Player Won!";
    const string PLAYER_LOST = "Player Lost!";
    //public TextMeshPro wonText;
    public  TextMeshProUGUI wonText;

    private void Awake()
    {
        loadAgainButton.onClick.AddListener(OnLoadAgainButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }


    public void PlayerWon()
    {
        visualBattlePart.SetActive(false);
        visualWonPart.SetActive(true);
    }

    public void PlayerLost()
    {
        visualBattlePart.SetActive(false);
        wonText.text = PLAYER_LOST;
        visualWonPart.SetActive(true);
    }

    private void OnLoadAgainButtonClick()
    {
        string name = SceneManager.GetActiveScene().name;
        LoadScene(name);
    }

    private void OnExitButtonClick()
    {
        LoadScene(0);
    }

    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadGameSceneCor(sceneNumber));
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadGameSceneCor(sceneName));
    }

    private IEnumerator LoadGameSceneCor(string sceneName)
    {
        AsyncOperation asyncLoaing = SceneManager.LoadSceneAsync(sceneName);
        asyncLoaing.allowSceneActivation = false;

        float timer = 0f;

        while (timer < fakeLoadTime || asyncLoaing.progress < 0.9f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        asyncLoaing.allowSceneActivation = true;
        while (asyncLoaing.progress < 0.99999f)
        {
            yield return null;
        }
    }

    private IEnumerator LoadGameSceneCor(int sceneNumber)
    {
        AsyncOperation asyncLoaing = SceneManager.LoadSceneAsync(sceneNumber);
        asyncLoaing.allowSceneActivation = false;

        float timer = 0f;

        while (timer < fakeLoadTime || asyncLoaing.progress < 0.9f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        asyncLoaing.allowSceneActivation = true;
        while (asyncLoaing.progress < 0.99999f)
        {
            yield return null;
        }
    }
}
