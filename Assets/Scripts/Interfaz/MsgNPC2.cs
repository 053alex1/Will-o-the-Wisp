using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgNPC2 : MonoBehaviour
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
        "Gracias por devolverme mi poder Dagda.",
        "El bosque es cada vez más hostil y las fuerzas Fomoré resurgen con más intensidad, pero ya casi nos has salvado a todos. ",
        "Solo Lugh y Morrigan están prisioneros todavía.",
        "Con ellos libres, los Tuatha Dé Danann recuperaremos todo nuestro poder,",
        "y seremos capaces de encerrar a los Fomoré para siempre… "
    };

    public string[] msgPulsaI = {
        "Pulsa I para interactuar"
    };

    public string GetMsgPulsaI() {
        return msgPulsaI[0];
    }

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
