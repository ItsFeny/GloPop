using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 10f;
    public string cuerdaTag = "cuerda";
    private bool isGrounded = true; // Variable para controlar si el personaje está en el suelo

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Añadimos la comprobación de si el personaje está en el suelo
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Cuando el personaje salta, indicamos que ya no está en el suelo
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cuerda"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) // Comprobamos si ha colisionado con el suelo
        {
            isGrounded = true; // Si colisiona con el suelo, indicamos que está en el suelo
        }
    }
}
