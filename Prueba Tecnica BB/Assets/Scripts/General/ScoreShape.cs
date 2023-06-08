using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreShape : MonoBehaviour
{
    private TextMeshProUGUI scoreBoard; 
    [SerializeField] private Vector2 winScale,loseScale; //escalas dependiendo si gana o pierde
    [SerializeField] private REvents gainEvent; //evento en el que gana puntos
    [SerializeField] private REvents[] loseEvent; //eventos en los que pierde puntos
    

    void Start()
    {
        scoreBoard = GetComponent<TextMeshProUGUI>();//referencia al mismo TextMesh
        gainEvent.GEvent += Gain;
        loseEvent[0].GEvent += Lose;
        loseEvent[1].GEvent += Lose;
    }

    void Gain()
    {
        scoreBoard.color = Color.blue; //se cambia el color al cual gano
        StartCoroutine(Scale(winScale)); //se escala , en este caso incrementa su tamaño
    }
    void Lose()
    {
        scoreBoard.color = Color.red; //se cambia el color al cual perdio
        StartCoroutine(Scale(loseScale));//se escala , en este caso reduce su tamaño
    }
    IEnumerator Scale(Vector2 scale)
    {
        transform.LeanScale(scale, 1f).setEaseOutQuart(); //se escala
        yield return new WaitForSeconds(0.5f);
        transform.LeanScale(Vector2.one, 1f).setEaseOutQuart(); //vuelve a su tamaño original
        scoreBoard.color = Color.white;//vuelve a su color original
    }
    private void OnDestroy()
    {
        gainEvent.GEvent -= Gain;
        loseEvent[0].GEvent -= Lose;
        loseEvent[1].GEvent -= Lose;
    }
}
