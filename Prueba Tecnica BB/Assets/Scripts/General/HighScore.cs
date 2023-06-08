using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI highScoreBoard;
    [SerializeField] private bool test; //para testear y reiniciar el puntaje mas alto
    
    void Start()
    {
        if (test == true)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        highScoreBoard = GetComponent<TextMeshProUGUI>();
        highScoreBoard.text = "High Score: " + PlayerPrefs.GetInt("HighScore"); //muestra el puntaje mas alto
    }  
}
