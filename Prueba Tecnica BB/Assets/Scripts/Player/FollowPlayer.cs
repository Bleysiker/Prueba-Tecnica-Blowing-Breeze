using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al transform del jugador
    [SerializeField] private float movementSmoothness = 0.5f; // Suavidad del movimiento de la cámara
    [SerializeField] private float rotationSpeed = 3f; // Velocidad de rotación de la cámara

    private Vector3 displacement; // Desplazamiento relativo entre la cámara y el jugador

    private float mouseX; // Movimiento horizontal del mouse
    private float mouseY; // Movimiento vertical del mouse

    private void Start()
    {
        displacement = transform.position - player.position; // Calcular el desplazamiento inicial
        Cursor.lockState = CursorLockMode.Locked; //bloquea el cursor en medio de la pantalla
    }

    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; // Obtener el movimiento horizontal del mouse
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; // Obtener el movimiento vertical del mouse

        mouseY = Mathf.Clamp(mouseY, -35f, 60f); // Limitar el ángulo de rotación vertical

        Quaternion rotationY = Quaternion.Euler(0f, mouseX, 0f); // Calcular la rotación horizontal del jugador
        Quaternion rotationX = Quaternion.Euler(mouseY, mouseX, 0f); // Calcular la rotación vertical del jugador

        player.rotation = Quaternion.Slerp(player.rotation, rotationY, movementSmoothness); // Interpolar suavemente la rotación horizontal del jugador

        Vector3 targetPosition = player.position + rotationX * displacement; // Calcular la posición objetivo de la cámara

        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothness); // Interpolar suavemente la posición de la cámara hacia la posición objetivo

        transform.LookAt(player.position); // Mantener la cámara mirando hacia el jugador
    }
}
