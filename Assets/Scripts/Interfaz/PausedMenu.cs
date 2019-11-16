﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject canvas;
    public GameObject background;
    public GameObject panel;
    public GameObject[] paneles;
    private bool isPaused = false;
    private bool primeraVegada = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if(isPaused)
        {
            ActivarMenu();
        }
        else
        {
            DesactivarMenu();
        }
    }
    public void ActivarMenu() 
    {
        GameObject.Find("Camera").GetComponent<CameraFollow>().enabled = false;
        GameObject.Find("Dagda").GetComponent<PlayerController>().enabled = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
        canvas.SetActive(true);
        background.SetActive(true);
        if (primeraVegada){
            pauseMenu.SetActive(true);
            primeraVegada = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DesactivarMenu()
    {
        GameObject.Find("Camera").GetComponent<CameraFollow>().enabled = true;
        GameObject.Find("Dagda").GetComponent<PlayerController>().enabled = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        canvas.SetActive(false);  //Falta fer que per a tots els fills els desacive
        paneles = GameObject.FindGameObjectsWithTag("Panel");
        foreach (GameObject panel in paneles)
        {
            panel.SetActive(false);
        }
        //background.SetActive(false);
        //pauseMenu.SetActive(false);
        primeraVegada = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CambiarPausa() 
    {
        isPaused = false;
    }
}