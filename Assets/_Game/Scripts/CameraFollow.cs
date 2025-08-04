using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetPosition;
    [SerializeField] Vector3 offsetRotation;

    public void OnInit(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offsetPosition;
        Quaternion targetRotation = Quaternion.Euler(offsetRotation);

        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }
}