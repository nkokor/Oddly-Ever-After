using UnityEngine;

public class KidMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Brzina kretanja
    public float rotationSpeed = 100f; // Brzina rotacije
    private Rigidbody rb;

    void Start()
    {
        // Dobavljanje Rigidbody komponente
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Rotacija
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Kretanje naprijed kada se pritisne 'Up Arrow'
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Kreće se naprijed u smeru trenutne rotacije
            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }
}
