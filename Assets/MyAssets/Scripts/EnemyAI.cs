using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]GameObject gameManGO;
    [SerializeField] GameManage gameManage;

    private void Awake()
    {
        gameManGO = GameObject.Find("GameManager");
        gameManage = gameManGO.GetComponent<GameManage>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //play attack animation
            Debug.Log("Attack");
            gameManage.Dead();
        }
    }
    private void FixedUpdate()
    {
        transform.LookAt(gameManage.playerInc.transform);
    }

}
