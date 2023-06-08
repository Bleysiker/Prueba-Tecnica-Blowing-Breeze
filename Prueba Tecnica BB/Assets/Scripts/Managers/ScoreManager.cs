using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private REvents addPoints; //evento de ganar puntos
    [SerializeField] private REvents[] losePoints; //eventos de perder puntos
    [SerializeField] private REvents savePoints;//evento de guardar puntos
    [SerializeField] private int points; //puntos actuales
    [SerializeField] private TextMeshProUGUI scoreBoard; //referencia al UI de los puntos
    [SerializeField] bool firstLvl; //variable para verificar si es el primer nivel

    void Start()
    {
        points = 0;
        if (firstLvl == false)
        {
            points = PlayerPrefs.GetInt("ActualScore"); //se carga el score anterior
        }
        scoreBoard.text = "Score: " + points;
        addPoints.GEvent += AddPoints; //se subscriben las funciones a los eventos
        losePoints[0].GEvent += RemovePoints;
        losePoints[1].GEvent += RemovePoints;
        savePoints.GEvent += Save;
    }

    void AddPoints() //funcion que resalta en el UI un score positivo
    {
        points++;
        scoreBoard.text = "Score: " + points;
    }
    void RemovePoints() //funcion que resalta en el UI un score negativo
    {
        points--;
        if (points < 0)
        {
            points = 0;
        }
        scoreBoard.text = "Score: " + points;
    }
    void Save()
    {
        PlayerPrefs.SetInt("ActualScore", points); //se guarda el score en la variable ActualScore
    }
    private void OnDestroy()
    {
        addPoints.GEvent -= AddPoints; //se dessubscriben las funciones a los eventos al ser destruidos
        losePoints[0].GEvent -= RemovePoints;
        losePoints[1].GEvent -= RemovePoints;
        savePoints.GEvent -= Save;
    }
}