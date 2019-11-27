using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds { JUMP, HIT }
public class Sound : MonoBehaviour
{
    public AudioSource[] sonidos; // Use this for initialization
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) //Deteccion de si es pulsada la barra espaciadora
            if (sonidos[(int)Sounds.JUMP].isPlaying) //Si el audio se esta reproduciendo
                sonidos[(int)Sounds.JUMP].Pause(); //Pausar la reproduccion
            else
                sonidos[(int)Sounds.JUMP].Play(); //Reproducir audio
    }
}