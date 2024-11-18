using UnityEngine;

public class Carriage : MonoBehaviour
{
    [SerializeField] private GameObject lava;

    private JointSystem jointSystem;

    private bool isLava;

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
            Destroy(gameObject);
            var joint = gameObject.GetComponent<ConfigurableJoint>();
            jointSystem.RemoveCarriage(gameObject, jointSystem.Joints.IndexOf(joint));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Lava"))
        {
            lava.SetActive(true);
            isLava = true;
        }
        if (other.GetComponent<Collider>().CompareTag("Water"))
        {
        }
    }
}
