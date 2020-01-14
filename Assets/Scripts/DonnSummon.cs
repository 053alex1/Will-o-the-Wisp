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
        GameObject donnObject = Instantiate(donn, new Vector3(885, 25, 95), Quaternion.identity) as GameObject;
        donnObject.transform.position = new Vector3(880f, 11f, 103f);
        donnObject.transform.rotation = Quaternion.Euler(new Vector3(0f, -85f, 0f));
        donnObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
