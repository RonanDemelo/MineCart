using UnityEngine;

public class MinecartRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject rail = null;
    [SerializeField]
    private GameObject minecart;
    [SerializeField]
    private GameObject player;

    float r;

    public float desieredRotation;
    public float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rail")
        {
            rail = other.gameObject;
            desieredRotation = rail.transform.eulerAngles.y;
        }
    }

    private void Update()
    {
        //minecart.transform.position = player.transform.position;

        float Angle = Mathf.SmoothDampAngle(minecart.transform.eulerAngles.y, desieredRotation, ref r, rotationSpeed);
        minecart.transform.rotation = Quaternion.Euler(0, Angle, 0);

    }
}
