﻿using System.Collections;
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
                if (Vector3.Distance(tr.position, npc.GetComponent<Transform>().position) < 16)
                {
                    npc.GetComponentInChildren<Animator>().SetTrigger("interaction");
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
