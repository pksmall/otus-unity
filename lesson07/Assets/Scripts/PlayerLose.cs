using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLose : MonoBehaviour
{
    public Button loadAgainButton;
    public float fakeLoadTime = 0f;
    public Button exitButton;
    public GameObject visualLose;


    private void Awake()
    {
        visualLose.SetActive(false);
        loadAgainButton.onClick.AddListener(OnLoadAgainButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public void PlayerLoseMenuView()
    {
        visualLose.SetActive(true);
    }

    private void OnLoadAgainButtonClick()
    {
        LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        LoadScene(0);
    }

    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadGameSceneCor(sceneNumber));
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
