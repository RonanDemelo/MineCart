using UnityEngine;

public class GameManage : MonoBehaviour
{
    [SerializeField] public bool gameHasEnded = false;
    [SerializeField] private float restartDelay = 2f;

    [SerializeField] private GameObject completeLevelUI;
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mapCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] public GameObject playerInc;
    [SerializeField] private float playerSpeed = 3f;

    public void StartLevel() 
    {
        mapCamera.SetActive(false);
        playerCamera.SetActive(true);
        SpawnPlayer();
    }

    public void KillPlayer()
    {
        Destroy(playerInc);
    }

    public void SpawnPlayer()
    {
        playerInc = Instantiate(player);
        Invoke("StartPlayer", restartDelay);
    }

    public void StartPlayer()
    {
        playerInc.GetComponent<PlayerController>().maxSpeed = playerSpeed;
    }

    public void FinishedLevel()
    {
        completeLevelUI.SetActive(true);
        EndGame();
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
        Invoke("PauseGame", restartDelay);
    }

    public void PauseGame()
    {
        pauseMenu.Pause();
    }
}
