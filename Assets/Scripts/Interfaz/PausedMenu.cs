using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject canvas;
    private bool isPaused = false;

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
        Time.timeScale = 0;
        AudioListener.pause = true;
        canvas.SetActive(true);
        pauseMenu.SetActive(true);
    }
    public void DesactivarMenu()
    {
        GameObject.Find("Camera").GetComponent<CameraFollow>().enabled = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        canvas.SetActive(false);
        pauseMenu.SetActive(false);
    }
}
