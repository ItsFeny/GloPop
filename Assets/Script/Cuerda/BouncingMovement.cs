using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position; // Inicia moviéndose hacia el punto B
    }

    void Update()
    {
        // Mueve la plataforma hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si la plataforma alcanza la posición objetivo, cambia la dirección
        if (transform.position == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            targetPosition = pointA.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja una línea que representa la trayectoria de la plataforma
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
