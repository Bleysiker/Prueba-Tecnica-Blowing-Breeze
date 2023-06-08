using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private REvents itemEvent; //referencia del evento que se va a disparar
    [SerializeField] bool disappear; //aca se marca si el objeto al final de la interaccion va a desaparecer o no

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemEvent.FireEvent(); //se dispara el evento
            this.gameObject.SetActive(!disappear); //desaparece el objeto o no
        }
    }
}
