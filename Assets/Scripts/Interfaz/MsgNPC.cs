using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgNPC : MonoBehaviour
{
    public GameObject dagda;
    public playerStats ps;
    int index = 0;

    public void Awake()
    {
        dagda = GameObject.FindGameObjectWithTag("Dagda");
        ps = dagda.GetComponent<playerStats>();
    }
    public string[] msg = { 
        "Cuánto tiempo Dagda.",
        "Hemos estado esperando tu regreso.",
        "Confío en que puedas rescatar a los otros."
    };
    public string GetMsg() {
        return msg[index];
    }
    public void okButon(){
        if (index == msg.Length - 1) {
            ps.Ultim = true;
        }else{
            index++;
        }
    }
    public void resetIndex() {
        index = 0;
    }
}
