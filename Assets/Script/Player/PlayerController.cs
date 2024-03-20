using UnityEngine;
using Photon.Pun; 

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviourPunCallbacks 
{
    public Rigidbody rb;
    public float jumpForce = 10f;
    public string cuerdaTag = "cuerda";
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!photonView.IsMine) 
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) // Verificar si este objeto es del jugador local
            return;

        if (other.CompareTag("Cuerda"))
        {
            photonView.RPC("Die", RpcTarget.All); // Llamar a una función remota para informar a todos los clientes que el jugador ha muerto
        }
    }

    [PunRPC]
    void Die()
    {
        gameObject.SetActive(false); // Desactivar el objeto en lugar de destruirlo
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!photonView.IsMine) // Verificar si este objeto es del jugador local
            return;

        if (collision.gameObject.CompareTag("Floor")) // Comprobamos si ha colisionado con el suelo
        {
            isGrounded = true; // Si colisiona con el suelo, indicamos que está en el suelo
        }
    }
}
