using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparisionNpc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comprobarSiMuerto();
    }

    private void comprobarSiMuerto()
    {
        dullahan = GameObject.Find("Dullahan");
        if(dullahan == null)
        {
            
        }
    }
}
