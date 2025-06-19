using Unity.Cinemachine;
using UnityEngine;

public class LightAttack : MonoBehaviour
{
    float rayLength = 10f;
    [SerializeField] LayerMask layerMask;
    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit _rayHit, rayLength, layerMask))
        {
            Debug.Log("LightOn");
            Debug.DrawRay(transform.position, transform.forward * rayLength, Color.yellow, 5f);
            if(_rayHit.transform.TryGetComponent(out EnemyAI enemy))
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
