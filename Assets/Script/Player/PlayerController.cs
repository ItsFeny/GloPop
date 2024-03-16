using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 10f;
    public string cuerdaTag = "cuerda";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cuerda"))
        {
            Destroy(gameObject);
        }
    }
}
