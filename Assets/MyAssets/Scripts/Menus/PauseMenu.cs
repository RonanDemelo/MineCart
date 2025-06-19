using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject deadMenuUI;

    public GameManage gameManager;

    [SerializeField] int SceneNo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        deadMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameManager.playerInc.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (gameManager.gameHasEnded == true)
        {
            deadMenuUI.SetActive(true);
            return;
        }
        pauseMenuUI.SetActive(true);
        gameManager.playerInc.SetActive(false);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneNo);
    }

    public void Retry()
    {
        gameManager.KillPlayer();
        gameManager.SpawnPlayer();
        Resume();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
