using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public CameraFollow cameraScript;
    public GameObject dagda;
    public PlayerController dagdaControll;
    public playerStats dagdaStats;
    public GameObject background;
    public GameObject panel;
    public GameObject panelControll;
    public GameObject panelOptions;
    public GameObject panelCreditos;
    public GameObject panelDeath;
    public GameObject[] paneles;
    public bool isPaused = false;
    private bool primeraVegada;
    private bool startTime = false;
    private bool started = false;
    public AudioMixer audioMixer;

    void Awake()
    {
        
    }

    private void Start()
    {
        dagda = GameObject.Find("Dagda");
        cameraScript = GameObject.Find("Camera").GetComponent<CameraFollow>();
        dagdaStats = dagda.GetComponent<playerStats>();
        dagdaControll = dagda.GetComponent<PlayerController>();
        panelOptions = GameObject.Find("panelOptions");
        panelControll = GameObject.Find("panelControll");
        panelDeath = GameObject.Find("panelDeath");
        background = GameObject.Find("background");
        pauseMenu = GameObject.Find("pauseMenu");
        panelCreditos = GameObject.Find("panelCreditos");

        DesactivarMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                isPaused = false;
                CambiarPausa();
            }
            else
            {
                isPaused = true;
                ActivarMenu();
            }
        }

        if (dagdaStats.isDead || GameObject.Find("Dagda") == null || !GameObject.Find("Dagda").active) {
            ActivarMenuMuerte();
        }
    }
    public void ActivarMenu() 
    {
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        //AudioListener.pause = true;
        background.SetActive(true);
        if (primeraVegada){
            pauseMenu.SetActive(true);
            primeraVegada = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Screen.lockCursor = false;
    }
    public void DesactivarMenu()
    {
        cameraScript.enabled = true;
        dagdaControll.enabled = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        background.SetActive(false);
        pauseMenu.SetActive(false);
        panelDeath.SetActive(false);
        panelCreditos.SetActive(false);
        primeraVegada = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CambiarPausa() 
    {
        isPaused = false;
        DesactivarMenu();
    }
    public void ActivarMenuMuerte() {
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        pauseMenu.SetActive(false);;
        panelCreditos.SetActive(false);
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        //AudioListener.pause = true;
        background.SetActive(true);
        panelDeath.SetActive(true);
        Screen.lockCursor = false;
    }

    public void ActivarCreditos() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        panelOptions.SetActive(false);
        panelControll.SetActive(false);
        pauseMenu.SetActive(false);
        panelDeath.SetActive(false);
        cameraScript.enabled = false;
        dagdaControll.enabled = false;
        Time.timeScale = 0;
        //AudioListener.pause = true;
        background.SetActive(true);
        panelCreditos.SetActive(true);
        Screen.lockCursor = false;
    }

    public void SetVolume (float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetVolumeEfectos (float volume) {
        audioMixer.SetFloat("efectos", volume);
    }

    public void SetVolumeMusica (float volume) {
        audioMixer.SetFloat("musica", volume);
    }

}
