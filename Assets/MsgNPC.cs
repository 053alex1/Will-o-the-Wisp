using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgNPC : MonoBehaviour
    {   
    int index = 0;
    public string[] msg = { 
        "Cuánto tiempo Dagda.",
        "Hemos estado esperando tu regreso.",
        "Confío en que puedas rescatar a los otros."
    };
    public string GetMsg() {
        return msg[index];
    }
    public void okButon(){
        index++;
    }
}
