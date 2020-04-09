using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLogic : MonoBehaviour
{
    public Slider progresBarSlider;
    public GameObject visualPart;
    public float fakeLoadTime = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        visualPart.SetActive(false);
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
        visualPart.SetActive(true);
        AsyncOperation asyncLoaing = SceneManager.LoadSceneAsync(sceneName);
        asyncLoaing.allowSceneActivation = false;

        float timer = 0f;

        while (timer < fakeLoadTime || asyncLoaing.progress < 0.9f)
        {
            timer += Time.deltaTime;
            SetProgressBarProgress(timer / fakeLoadTime);

            yield return null;
        }

        asyncLoaing.allowSceneActivation = true;
        while(asyncLoaing.progress < 0.99999f)
        {
            yield return null;
        }
        visualPart.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator LoadGameSceneCor(int sceneNumber)
    {
        visualPart.SetActive(true);
        AsyncOperation asyncLoaing = SceneManager.LoadSceneAsync(sceneNumber);
        asyncLoaing.allowSceneActivation = false;

        float timer = 0f;

        while (timer < fakeLoadTime || asyncLoaing.progress < 0.9f)
        {
            timer += Time.deltaTime;
            SetProgressBarProgress(timer / fakeLoadTime);

            yield return null;
        }

        asyncLoaing.allowSceneActivation = true;
        while (asyncLoaing.progress < 0.99999f)
        {
            yield return null;
        }
        visualPart.SetActive(false);
    }

    private void SetProgressBarProgress(float progress)
    {
        progresBarSlider.value = progress;
    }

}
