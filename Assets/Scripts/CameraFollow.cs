using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position - target.forward * _offset.z + target.up * _offset.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        Quaternion targetRotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.LookAt(target);
    }
}
