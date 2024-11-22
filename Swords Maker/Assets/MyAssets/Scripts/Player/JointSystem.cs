using SwordsMaker;
using System.Collections.Generic;
using UnityEngine;

public class JointSystem : MonoBehaviour
{
    public List<ConfigurableJoint> Joints;

    private Manager manager;

    private void Awake()
    {
        Joints = new List<ConfigurableJoint>();

        manager = FindFirstObjectByType<Manager>();

        Time.timeScale = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Node"))
        {
            var obj = collision.gameObject;

            if (obj == null) return;

            AddCarriage(obj);
        }
        if (collision.collider.CompareTag("Trap"))
        {
            Time.timeScale = 0;

            manager.Panel.SetActive(true);
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

    public void RemoveCarriage(GameObject @object, int index)
    {
        while (Joints.Count > index)
        {
            if (Joints.Count > 0)
            {
                var joint = Joints[^1].gameObject;

                Joints.RemoveAt(Joints.Count - 1);

                Destroy(joint);

                continue ;
            }
            break ;
        }
    }
}
