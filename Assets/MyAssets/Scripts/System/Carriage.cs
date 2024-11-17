using UnityEngine;

public class Carriage : MonoBehaviour
{
    private JointSystem jointSystem;

    private void Awake()
    {
        jointSystem = FindAnyObjectByType<JointSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Node"))
        {
            if (jointSystem.Joints.Contains(gameObject.GetComponent<ConfigurableJoint>()))
                jointSystem.AddCarriage(collision.gameObject);
        }
        if (collision.collider.CompareTag("Trap"))
        {
            jointSystem.RemoveCarriage();
            Destroy(gameObject);
        }
    }
}
