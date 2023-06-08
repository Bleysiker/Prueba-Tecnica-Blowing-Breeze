using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private REvents endLevel;
    [SerializeField] private Vector2 newPosition; //la posicion a la que se mueve fuera de pantalla
    
    void Start()
    {
        transform.LeanMoveLocal(newPosition, 2f).setEaseOutQuart(); //despalzamiento de la pantalla negra hacia afuera
        endLevel.GEvent += MoveScreen;
    }

    void MoveScreen()
    {
        transform.LeanMoveLocal(Vector2.zero, 2f).setEaseOutQuart(); //despalzamiento de la pantalla negra cerrando
    }
    private void OnDestroy()
    {
        endLevel.GEvent -= MoveScreen;
    }
}
