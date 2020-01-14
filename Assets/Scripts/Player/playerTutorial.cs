using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerTutorial : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public GameObject msgPanel;
    public Text msgText;
    public GameObject canvas;
    public GameObject npc;
    public GameObject enemy;
    GameObject player;
    public GameObject burbuja;
    public MsgTutorial msgTut;
    public playerStats ps;
    private changelayer cl;
    private PlayerController dc;
    private CameraFollow cameraScript; 
    public bool condicio1 = false;
    public bool condicio1Prev = false;
    public bool condicio2Prev = false;
    public bool condicio2 = false;
    public bool lliure = false;
    
    public bool habilitarCondicio2 = false;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        msgTut = player.GetComponent<MsgTutorial>();
        ps = player.GetComponent<playerStats>();
        dc = player.GetComponent<PlayerController>();
        cameraScript = GameObject.Find("Camera").GetComponent<CameraFollow>();

        msgPanel = GameObject.FindGameObjectWithTag("panelMensaje");
        //cl = GameObject.Find("LevelChanger").GetComponentInChildren<changelayer>();
    }
    void Start()
    {
        enemy.SetActive(false);
        msgPanel.SetActive(true);
        tr = player.transform.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
        ps.isGrounded = true;
        ps.isRunning = false;
        string msg = msgTut.GetMsg();
        msgText.text = msg;
    }
    void Update()
    {
        if(!lliure) {
            bloquejat();
        }else {
            desbloquejat();
        }
        Tutorial();
        comprovantCondicio1();
    }

    void bloquejat() {
        cameraScript.enabled = false;
        dc.enabled = false;
        Time.timeScale = 0;
    }
    void desbloquejat() {
        cameraScript.enabled = true;
        dc.enabled = true;
        Time.timeScale = 1;
    }

    void Tutorial() {
        
        if (!ps.Ultim)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if(msgTut.index < 2) {
                    if (msgTut.index >= 0){
                    msgTut.okButon();
                    string msg = msgTut.GetMsg();
                    msgText.text = msg;
                    }
                }else if(msgTut.index < 4) {
                        if (msgTut.index >= 2){
                        lliure = true;
                        msgTut.okButon();
                        string msg = msgTut.GetMsg();
                        msgText.text = msg;
                    }
                }
                if(msgTut.index < 7){
                    if (msgTut.index >= 5){
                        if(condicio1){
                            msgTut.okButon();
                            string msg = msgTut.GetMsg();
                            msgText.text = msg;
                        }
                    }
                }
                if(msgTut.index < 9){
                    if (msgTut.index >= 8){
                        if(condicio2){
                            msgTut.okButon();
                            string msg = msgTut.GetMsg();
                            msgText.text = msg;
                        }
                    }
                }
            }
            else
            {
                if(msgTut.index < 5){
                    if (msgTut.index >= 4){
                        if(condicio1Prev){
                            msgTut.okButon();
                            string msg = msgTut.GetMsg();
                            msgText.text = msg;
                            condicio1Prev = false;
                            condicio1 = true;
                        }
                    }
                }
                else{
                    if(condicio1){
                        //enemy = GameObject.FindGameObjectWithTag("Enemy");
                        if(enemy == null){
                            condicio2Prev = true;
                        }else{
                            enemy.SetActive(true);
                        }
                        //habilitarCondicio2 = true;
                        if(msgTut.index < 9) {
                            if (msgTut.index >= 8){
                                if (condicio2Prev){
                                msgTut.okButon();
                                string msg = msgTut.GetMsg();
                                msgText.text = msg;
                                condicio2Prev = false;
                                condicio2 = true;
                                }
                            }
                        }
                    }
                }  
            }
        }
        else {
            Debug.Log("MENSAJE ULTIMO");
            cl.FadeToLevel(); 
            //POSAR CODI CANVI ESCENA ACI!!!!!!!
        }
    }
    void comprovantCondicio1() {
        burbuja = GameObject.FindGameObjectWithTag("Bubble");
        if(burbuja != null){
            if(burbuja.GetComponent<BurbujaController>().cont > 0){
                condicio1Prev = true;
            }
        }
        
    }
}
