using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position; // Inicia movi�ndose hacia el punto B
    }

    void Update()
    {
        // Mueve la plataforma hacia la posici�n objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si la plataforma alcanza la posici�n objetivo, cambia la direcci�n
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
        // Dibuja una l�nea que representa la trayectoria de la plataforma
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
