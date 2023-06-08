using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Velocidad de movimiento normal
    [SerializeField] private float rotationSpeed = 10f; // Velocidad de rotación
    [SerializeField] private float jumpForce = 5f; // Fuerza de salto
    [SerializeField] private float knockBackForce = 5f; //Fuerza del KnockBack
    [SerializeField] private REvents knockBack; //referencia al evento

    private Rigidbody rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        knockBack.GEvent += KnockBack;
    }

    private void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal"); //se toman los valores de las teclas WASD
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalMovement * movementSpeed +
                           transform.right * horizontalMovement * movementSpeed; //se establece el vector velocidad con respecto a los valores que se obtuvieron

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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //salta con respecto a un vector que va hacia arriba
    }

    private void KnockBack()
    {
        rb.AddForce(((Vector3.up+(-transform.forward))) * knockBackForce, ForceMode.Impulse); //el knockback se genera del vector resultante entre el vector que apunta hacia arriba y el vector que apunta atras
        StartCoroutine(KnockBackTime(1.2f));
    }

    IEnumerator KnockBackTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.isKinematic = true;//se frena la fuerza generada por el knockback
        rb.isKinematic = false;
    }
    private void OnDestroy()
    {
        knockBack.GEvent -= KnockBack;
    }
}



