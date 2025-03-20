using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Snelheid van de speler
    public float jumpForce = 5f; // Kracht van de sprong
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Haal de Rigidbody op
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // A & D / Links & Rechts
        float moveZ = Input.GetAxis("Vertical");   // W & S / Vooruit & Achteruit

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ) * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
