using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManage : MonoBehaviour
{
    public bool gameHasEnded = false;
    [SerializeField] private float restartDelay = 3f;
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mapCamera;
    [SerializeField] private GameObject playerCamera;
    public GameObject playerInc;
    public PlayerController playerCon;
    private float lastScore;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private float playerSpeed = 3f;

    public void StartLevel() 
    {
        mapCamera.SetActive(false);
        playerCamera.SetActive(true);
        Invoke("SpawnPlayer", 0.5f);
    }

    public void KillPlayer()
    {
        lastScore = playerCon.score - 5f;
        Destroy(playerInc);
    }

    public void SpawnPlayer()
    {
        gameHasEnded = false;
        playerInc = Instantiate(player);
        playerCon = playerInc.GetComponent<PlayerController>();
        playerCon.score = lastScore;
        Invoke("StartPlayer", restartDelay);
    }

    public void StartPlayer()
    {
        playerInc.GetComponent<PlayerController>().maxSpeed = playerSpeed;
    }

    public void FinishedLevel()
    {
        scoreText.text = "Score: " + playerCon.score.ToString("F0");
        Invoke("WinGame", 0.25f);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
        }
    }

    public void Dead()
    {
        EndGame();
        KillPlayer();
        Invoke("PauseGame", restartDelay);
    }

    public void PauseGame()
    {
        pauseMenu.Pause();
    }

    public void WinGame()
    {
        pauseMenu.Win();
    }
}
