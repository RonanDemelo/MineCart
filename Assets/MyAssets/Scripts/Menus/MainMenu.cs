using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image image;

    public void PlayEasy()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(1));
    }

    public void PlayHard()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(2));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            image.fillAmount = progress;

            yield return null;
        }
    }
}
