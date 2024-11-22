using SwordsMaker;
using UnityEngine;
using DG.Tweening;

public class Carriage : MonoBehaviour
{
    private Manager manager;

    [SerializeField] private ConfigurableJoint joint;

    [SerializeField] private JointSystem jointSystem;

    [SerializeField] private GameObject concrete;
    [SerializeField] private GameObject carriage;
    [SerializeField] private GameObject lava;

    private Renderer lavaRenderer;

    [SerializeField] private Color32 waterColor;

    private int index;

    private void Awake()
    {
        jointSystem = FindAnyObjectByType<JointSystem>();
        joint = GetComponent<ConfigurableJoint>();

        manager = FindAnyObjectByType<Manager>();

        concrete = transform.Find("Concrete").gameObject;
        carriage = transform.Find("Carriage").gameObject;
    }

    private void Start()
    {
        lavaRenderer = lava.GetComponent<Renderer>();
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
            var joint = GetComponent<ConfigurableJoint>();

            jointSystem.RemoveCarriage(gameObject, jointSystem.Joints.IndexOf(joint));
        }
        if (collision.collider.CompareTag("Finish"))
        {
            var joint = GetComponent<ConfigurableJoint>();

            jointSystem.RemoveCarriage(gameObject, jointSystem.Joints.IndexOf(joint));

            if (index == 4)
            {
                index = 5;

                manager.nodeCount--;

                if (collision.gameObject.name == "Speed")
                {
                    Debug.Log("Speed");
                }
                if (collision.gameObject.name == "Damage")
                {
                    Debug.Log("Damage");
                }
            }
            if (manager.nodeCount == 0)
            {
                Debug.Log("Level Completed!");

                manager.Panel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Lava"))
        {
            index = 1;

            lava.SetActive(true);
        }
        if (other.GetComponent<Collider>().CompareTag("Water"))
        {
            if (index == 1)
            {
                index = 2;

                lavaRenderer.material.color = waterColor;

                joint.anchor = new Vector3(0, 0, 0.28f);

                joint.connectedAnchor = new Vector3(0, 0, -0.28f);
            }
        }
        if (other.GetComponent<Collider>().CompareTag("Press"))
        {
            if (index == 2)
            {
                index = 3;

                carriage.transform.DOScaleY(0.5f, 0.1f);

                Animator animator = other.transform.parent.GetComponent<Animator>();

                animator?.SetTrigger("Press");
            }
        }
        if (other.GetComponent<Collider>().CompareTag("Emery"))
        {
            if (index == 3)
            {
                index = 4;

                concrete.SetActive(true);

                carriage.SetActive(false);

                manager.nodeCount++;
            }
        }
    }
}
