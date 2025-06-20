using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]GameObject gameManGO;
    [SerializeField] GameManage gameManage;
    [SerializeField] Animator animator;

    private void Awake()
    {
        gameManGO = GameObject.Find("GameManager");
        gameManage = gameManGO.GetComponent<GameManage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Attack");
            animator.Play("Attack");
            gameManage.Dead();
        }
        
    }

    private void FixedUpdate()
    {
        transform.LookAt(gameManage.playerInc.transform);
    }

}
