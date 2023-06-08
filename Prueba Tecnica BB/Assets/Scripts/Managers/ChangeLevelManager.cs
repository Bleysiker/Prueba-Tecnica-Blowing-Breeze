using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeLevelManager : MonoBehaviour
{
    [SerializeField] private REvents endLevel; //referencia al evento que termina el nivel
    [SerializeField] private string nextLevel; //nombre de la siguiente escena
    void Start()
    {
        endLevel.GEvent += ChangeLevel; //la funcion se subscribe al evento
    }
    
    void ChangeLevel()
    {
        StartCoroutine("ChangingLevel");
    }
    IEnumerator ChangingLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevel); //se carga la escena
    }
    private void OnDestroy()
    {
        endLevel.GEvent -= ChangeLevel; //la funcion se desubscribe del evento
    }
}
