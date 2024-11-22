using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Vector3 offset;

    [SerializeField] private Transform target;

    [SerializeField] private float smoothTime;

    private bool isFinish;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    private void Update()
    {
        if (!isFinish)
        {
            Physics.Raycast(target.position, target.forward, out RaycastHit hit, 5, LayerMask.GetMask("Finish"));
            if (hit.collider != null)
            {
                Debug.DrawLine(target.position, hit.point, Color.red);

                if (hit.collider.CompareTag("Finish"))
                {
                    Camera.main.fieldOfView = 50;

                    isFinish = true;
                }
            }
        }
    }
}
