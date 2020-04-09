using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLogic : MonoBehaviour
{
    public GameObject visualBattlePart;
    public GameObject visualPausePart;
    public GameObject visualSettingPart;
    public float fakeLoadTime = 0f;

    public Button cancelButton;
    public Button loadAgainButton;
    public Button exitButton;
    public Button settingButton;


    private void Awake()
    {
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        loadAgainButton.onClick.AddListener(OnLoadAgainButtonClick);
        settingButton.onClick.AddListener(OnSettingButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public void OnPauseButtonClick()
    {
        Debug.Log("Awake: " + SceneManager.GetActiveScene().name);

        visualPausePart.SetActive(true);
        visualBattlePart.SetActive(false);
    }

    private void OnCancelButtonClick()
    {
        visualPausePart.SetActive(false);
        visualBattlePart.SetActive(true);
    }
    private void OnSettingButtonClick()
    {
        visualBattlePart.SetActive(false);
        visualSettingPart.SetActive(true);
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
