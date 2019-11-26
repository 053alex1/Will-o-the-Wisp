﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public CameraFollow cameraScript;
    public GameObject dagda;
    public PlayerController dagdaControll;
    public playerStats dagdaStats;
    public GameObject background;
    public GameObject panel;
    public GameObject panelStart;
    public GameObject panelControll;
    public GameObject panelOptions;
    public GameObject panelDeath;
    public GameObject[] paneles;
    public bool isPaused = false;
    private bool primeraVegada;
    private bool startTime = true;
    private bool started = false;

    void Start() {
        //paneles = GameObject.FindGameObjectsWithTag("Panel");
        panelStart = GameObject.FindGameObjectWithTag("panelStart");
        dagda = GameObject.Find("Dagda");
        cameraScript = GameObject.Find("Camera").GetComponent<CameraFollow>();
        dagdaStats = dagda.GetComponent<playerStats>();
        dagdaControll = dagda.GetComponent<PlayerController>();
        panelOptions = GameObject.FindGameObjectWithTag("panelOptions");
        panelControll = GameObject.FindGameObjectWithTag("panelControll");
        panelDeath = GameObject.FindGameObjectWithTag("panelDeath");
        background = GameObject.FindGameObjectWithTag("background");
        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
    }

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

        if(startTime){
            ActivarMenuStart();
            started = true;
        }
        if(started) {
            if (Input.anyKey)
            {
                DesactivarMenu();
                startTime = false;
                started = false; 
            }
        }


        if (dagdaStats.isDead || GameObject.Find("Dagda") == null ) {
            ActivarMenuMuerte();
        }
    }
    public void ActivarMenu() 
    {
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
        //canvas.SetActive(true);
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
        cameraScript.enabled = true;
        dagdaControll.enabled = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        // foreach (GameObject panel in paneles)
        // {
        //     panel.SetActive(false);
        // }
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        background.SetActive(false);
        pauseMenu.SetActive(false);
        panelStart.SetActive(false);
        panelDeath.SetActive(false);
        primeraVegada = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CambiarPausa() 
    {
        isPaused = false;
    }
    public void ActivarMenuMuerte() {
        
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        background.SetActive(false);
        pauseMenu.SetActive(false);
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
        background.SetActive(true);
        panelDeath.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ActivarMenuStart() {
        
        primeraVegada = true;
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        pauseMenu.SetActive(false);
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
        background.SetActive(true);
        panelStart.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
