using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainSound; //referencia al audioSource de la musica de fondo
    [SerializeField] private AudioSource AdditionalSound; //referencia al audioSource del sonido adicional
    [SerializeField] private AudioClip losePoint;  //los audioclips de los sonidos adicionales
    [SerializeField] private AudioClip gainPoint;
    [SerializeField] private AudioClip finishLevel;
    [Range(0,20)]
    [SerializeField] private int soundVolume;   //volumen que se le va a restar al sonido del fondo
    [SerializeField] private REvents gainEvent; //referencias a los eventos del juego
    [SerializeField] private REvents[] loseEvent;
    [SerializeField] private REvents finishEvent;

    void Start()
    {
        gainEvent.GEvent += GainPointSound; // se subscriben las funciones a sus respectivos eventos
        loseEvent[0].GEvent += LosePointSound;
        loseEvent[1].GEvent += LosePointSound;
        finishEvent.GEvent += FinishLevelSound;
    }
    void GainPointSound()
    {
        ChangeSound(gainPoint, 0.02f);
    }
    void LosePointSound()
    {
        ChangeSound(losePoint, 1.02f);
    }
    void FinishLevelSound()
    {
        ChangeSound(finishLevel, 1f);
    }
    void ChangeSound(AudioClip sound,float soundTime) //se especifica el clip que va a reproducir y el tiempo que dura
    {
        mainSound.volume -= soundVolume;//se resta el volumen
        AdditionalSound.Stop();//se detiene el sonido que tiene 
        AdditionalSound.clip = sound;// se cambia el sonido
        AdditionalSound.Play();// se reproduce el sonido
        StartCoroutine(WaitAndRestore(soundTime));//empieza la corrutina en donde pasado un tiempo se restaura el volumen de la musica de fondo
    }
    IEnumerator WaitAndRestore(float soundTime)
    {
        yield return new WaitForSeconds(soundTime);
        mainSound.volume += soundVolume;
    }
    private void OnDestroy()
    {
        gainEvent.GEvent -= GainPointSound; // se desubscriben las funciones de sus respectivos eventos
        loseEvent[0].GEvent -= LosePointSound;
        loseEvent[1].GEvent -= LosePointSound;
        finishEvent.GEvent -= FinishLevelSound;
    }
}