using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        msgPanel = GameObject.FindGameObjectWithTag("panelMensaje");
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
    }

   void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            npc = GameObject.FindGameObjectWithTag("NPC");
            if (npc != null)
            {
                if (Vector3.Distance(tr.position, npc.transform.position) < 16)
                {
                    //npc.GetComponentInChildren<Animator>().SetTrigger("interaction");
                    if (msgPanel.activeSelf == true)
                    {
                        npc.GetComponent<MsgNPC>().okButon();
                        if (!ps.Ultim) {
                            string msg = npc.GetComponent<MsgNPC>().GetMsg();
                            msgText.text = msg;
                        }else{
                            npc.GetComponent<MsgNPC>().resetIndex();
                            msgPanel.SetActive(false);
                            ps.Ultim = false;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                }
            }
        }
    }
}
