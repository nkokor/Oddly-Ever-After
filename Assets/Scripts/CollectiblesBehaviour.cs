using UnityEngine;

public class FloatingCollectible : MonoBehaviour
{
    public float floatingHeight = 0.2f;
    public float floatingSpeed = 1.8f;  
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}

