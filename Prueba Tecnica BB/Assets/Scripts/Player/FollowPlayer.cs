using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Referencia al transform del jugador
    [SerializeField] private float movementSmoothness = 0.5f; // Suavidad del movimiento de la c�mara
    [SerializeField] private float rotationSpeed = 3f; // Velocidad de rotaci�n de la c�mara

    private Vector3 displacement; // Desplazamiento relativo entre la c�mara y el jugador

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

        mouseY = Mathf.Clamp(mouseY, -35f, 60f); // Limitar el �ngulo de rotaci�n vertical

        Quaternion rotationY = Quaternion.Euler(0f, mouseX, 0f); // Calcular la rotaci�n horizontal del jugador
        Quaternion rotationX = Quaternion.Euler(mouseY, mouseX, 0f); // Calcular la rotaci�n vertical del jugador

        player.rotation = Quaternion.Slerp(player.rotation, rotationY, movementSmoothness); // Interpolar suavemente la rotaci�n horizontal del jugador

        Vector3 targetPosition = player.position + rotationX * displacement; // Calcular la posici�n objetivo de la c�mara

        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothness); // Interpolar suavemente la posici�n de la c�mara hacia la posici�n objetivo

        transform.LookAt(player.position); // Mantener la c�mara mirando hacia el jugador
    }
}
