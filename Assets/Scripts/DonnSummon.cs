using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonnSummon : MonoBehaviour
{
    [SerializeField]
    private GameObject donn;
    [SerializeField]
    private GameObject donnEffect;
    public bool comprobar = false;
    public bool stopComprobar = false;
    private GameObject dullahanObject;

    // Update is called once per frame
    void Update()
    {
        if(!stopComprobar){
            if(comprobar){
                comprobarSiMuerto();
            }
        }
    }

    private void comprobarSiMuerto()
    {
        dullahanObject = GameObject.Find("Dullahan(Clone)");
        if(dullahanObject == null)
        {
            DonnSummoning();
            stopComprobar = true;
        }
    }
    
    void DonnSummoning() {
        GameObject donnObject = Instantiate(donn, new Vector3(885, 27, 95), Quaternion.Euler(new Vector3(0f, -85f, 0f))) as GameObject;
    }
}
