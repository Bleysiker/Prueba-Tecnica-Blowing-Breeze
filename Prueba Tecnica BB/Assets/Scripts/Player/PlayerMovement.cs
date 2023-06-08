using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Velocidad de movimiento normal
    [SerializeField] private float rotationSpeed = 10f; // Velocidad de rotación
    [SerializeField] private float jumpForce = 5f; // Fuerza de salto
    [SerializeField] private float knockBackForce = 5f; // Fuerza del KnockBack
    [SerializeField] private REvents knockBack; // Referencia al evento

    private Rigidbody rb;
    [SerializeField] private bool isGrounded = true; // Indica si el jugador está en el suelo

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        knockBack.GEvent += KnockBack;
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); // Se toman los valores de las teclas WASD
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement * movementSpeed +
                           transform.right * horizontalMovement * movementSpeed; // Se establece el vector velocidad con respecto a los valores obtenidos

        rb.MovePosition(transform.position + movement * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        // Verificar si se presionó la tecla de espacio para saltar y si el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Salta con respecto a un vector que apunta hacia arriba
        isGrounded = false; // El jugador ya no está en el suelo
    }

    private void KnockBack()
    {
        rb.AddForce(((Vector3.up + (-transform.forward))) * knockBackForce, ForceMode.Impulse); // El knockback se genera del vector resultante entre el vector que apunta hacia arriba y el vector que apunta hacia atrás
        StartCoroutine(KnockBackTime(1.2f));
    }

    IEnumerator KnockBackTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.isKinematic = true; // Se frena la fuerza generada por el knockback
        rb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // El jugador está en el suelo
        }
    }

    private void OnDestroy()
    {
        knockBack.GEvent -= KnockBack;
    }
}





