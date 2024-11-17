using System.Collections.Generic;
using UnityEngine;

public class JointSystem : MonoBehaviour
{
    public List<ConfigurableJoint> Joints;

    private void Awake()
    {
        Joints = new List<ConfigurableJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Node"))
        {
            var obj = collision.gameObject;

            if (obj == null) return;

            AddCarriage(obj);
        }
    }

    public void AddCarriage(GameObject @object)
    {
        if (@object.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
        }
        if (@object.TryGetComponent<ConfigurableJoint>(out var joint))
        {
            bool isFirstAddition = Joints.Count == 0;

            if (!Joints.Contains(joint))
            {
                Joints.Add(joint);

                if (isFirstAddition)
                {
                    GetComponent<ConfigurableJoint>().connectedBody = rb;
                }
                else
                {
                    Joints[^2].connectedBody = rb;
                    Joints[^2].zMotion = ConfigurableJointMotion.Limited;
                }
            }
        }
    }

    public void RemoveCarriage()
    {
        if (Joints.Count == 0) return;

        var lastJoint = Joints[^1];
        Joints.Remove(lastJoint);

        if (Joints.Count == 0) return;

        Joints[^1].connectedBody = null;
        Joints[^1].zMotion = ConfigurableJointMotion.Free;
    }
}
