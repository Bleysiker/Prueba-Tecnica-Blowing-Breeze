using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector2.zero; //reduce las instrucciones a un tamaño imperceptible
    }
    public void AppearInst()
    {
        StartCoroutine(AppearInstruction());
    }
    IEnumerator AppearInstruction()
    {
        transform.LeanScale(Vector2.one, 1f).setEaseOutQuart(); //escala las instrucciones al tamaño original
        yield return new WaitForSeconds(10f);
        transform.LeanScale(Vector2.zero, 1f).setEaseOutQuart(); //despues de 10 segundos los reduce
    }
}
