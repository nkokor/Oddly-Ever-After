using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    public float sensitivity = 4f;
    public float maxOffsetX = 10.0f;
    public float maxOffsetY = 10.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f) * 2;
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f) * 2;

        float offsetX = Mathf.Clamp(mouseX * sensitivity, -maxOffsetX, maxOffsetX);
        float offsetY = Mathf.Clamp(mouseY * sensitivity, -maxOffsetY, maxOffsetY);

        transform.position = initialPosition + new Vector3(offsetX, offsetY, 0);
    }
}
