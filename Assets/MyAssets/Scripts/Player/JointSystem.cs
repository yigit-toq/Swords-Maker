using System.Collections.Generic;
using UnityEngine;

public class JointSystem : MonoBehaviour
{
    [SerializeField] private List<ConfigurableJoint> joints;

    private void Awake()
    {
        joints = new List<ConfigurableJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Node"))
        {
            joints.Add(collision.collider.GetComponent<ConfigurableJoint>());
        }
    }
}
