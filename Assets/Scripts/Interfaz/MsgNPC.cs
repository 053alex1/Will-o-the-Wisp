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
        "No tengas miedo de lo que te hace estremecerte.",
        "Mantente de pie valiente, defiéndete, eres un guerrero.",
        "Siempre estaré en deuda contigo, amigo mío."
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
