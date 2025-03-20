using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Beweeg snelheid
    public float rotationSpeed = 100f; // Rotatie snelheid

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Voorkomt dat de speler omvalt
    }

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float moveZ = Input.GetAxis("Vertical"); // W & S (Vooruit/Achteruit)
        Vector3 moveDirection = transform.forward * moveZ * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }

    void Rotate()
    {
        float rotateY = Input.GetAxis("Horizontal"); // A & D (Roteren)
        transform.Rotate(0, rotateY * rotationSpeed * Time.deltaTime, 0);
    }
}
    