using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Velocidad de movimiento normal
    [SerializeField] private float rotationSpeed = 10f; // Velocidad de rotación
    [SerializeField] private float jumpForce = 5f; // Fuerza de salto

    private Rigidbody rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement * movementSpeed +
                           transform.right * horizontalMovement * movementSpeed;

        rb.MovePosition(transform.position + movement * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si se presionó la tecla de espacio para saltar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    
}



