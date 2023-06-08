using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    [SerializeField] REvents playGame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; 
    }

    public void StartGame()
    {
        playGame.FireEvent(); //se dispara para que la pantalla negra baje
        StartCoroutine(WaitSesion());
    }

    IEnumerator WaitSesion()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("lvl 1"); //inicia el juego
    }
}
