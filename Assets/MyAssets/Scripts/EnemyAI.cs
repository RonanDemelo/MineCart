using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]GameManage gameManage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //play attack animation
            Debug.Log("Attack");
        }
    }
    private void FixedUpdate()
    {
        transform.LookAt(gameManage.playerInc.transform);
    }

}
