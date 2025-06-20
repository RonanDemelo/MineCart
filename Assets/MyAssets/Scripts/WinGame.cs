using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] GameObject gameManGO;
    [SerializeField] GameManage gameManage;

    private void Awake()
    {
        gameManGO = GameObject.Find("GameManager");
        gameManage = gameManGO.GetComponent<GameManage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Win");
            gameManage.FinishedLevel();
        }

    }
}
