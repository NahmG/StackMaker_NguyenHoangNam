using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetPosition;
    [SerializeField] Vector3 offsetRotation;
    [SerializeField] float speed = 15;

    public void OnInit(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offsetPosition;
        Quaternion targetRotation = Quaternion.Euler(offsetRotation);

        transform.rotation = targetRotation;
        transform.position = Vector3.Lerp(transform.position, targetPosition + offsetPosition, speed * Time.deltaTime);
    }
}