using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerInteraction : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public GameObject msgPanel;
    public Text msgText;
    public GameObject canvas;
    public GameObject npc;
    GameObject player;
    public GameObject burbuja;
    public playerStats ps;
    private changelayer cl;
    private bool hablando = false;
    private bool primeraVegada = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        cl = GameObject.Find("LevelChanger").GetComponentInChildren<changelayer>();
    }
    void Start()
    {
        msgPanel.SetActive(false);
        tr = player.transform.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
        ps.isGrounded = true;
        ps.isRunning = false;
    }
    void Update()
    {
        Interaction();
        if(!hablando){
            pulsaI();
        }
    }

    void pulsaI()
    {
        npc = GameObject.FindGameObjectWithTag("NPC");
        if (npc != null)
        {
            if (Vector3.Distance(tr.position, npc.transform.position) < 16)
            {
                msgPanel.SetActive(true);
                string msg = npc.GetComponent<MsgNPC>().GetMsgPulsaI();
                msgText.text = msg;
            }else {
                msgPanel.SetActive(false);
            }
        }
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            hablando = true;

            Debug.Log("Interaction triggered");
            npc = GameObject.FindGameObjectWithTag("NPC");
            if (npc != null)
            {
                Debug.Log("Found Cernunnos");
                Debug.Log("Distance: " + Vector3.Distance(tr.position, npc.transform.position));
                if (Vector3.Distance(tr.position, npc.transform.position) < 16)
                {
                    Debug.Log("Cernunnos talking");
                    npc.GetComponentInChildren<Animator>().SetTrigger("interaction");

                    if (primeraVegada) {
                        msgPanel.SetActive(false);
                        primeraVegada = false;
                    }
                    if (msgPanel.activeSelf == true)
                    {
                        npc.GetComponent<MsgNPC>().okButon();
                        if (!ps.Ultim)
                        {
                            string msg = npc.GetComponent<MsgNPC>().GetMsg();
                            msgText.text = msg;
                        }
                        else
                        {
                            npc.GetComponent<MsgNPC>().resetIndex();
                            msgPanel.SetActive(false);
                            ps.Ultim = false;
                            //hablando = false; Com volem canviar el nivell no fa falta canviar esta variable
                            cl.FadeToLevel();
                        }
                    }
                    else
                    {
                        msgPanel.SetActive(true);
                        string msg = npc.GetComponent<MsgNPC>().GetMsg();
                        msgText.text = msg;
                    }
                }
                else
                {
                    npc.GetComponent<MsgNPC>().resetIndex();
                    msgPanel.SetActive(false);
                    primeraVegada = true;
                    hablando = false;
                }
            }
        }
    }
}
