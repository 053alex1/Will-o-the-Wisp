using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject cernunnos;
    public GameObject summonEffect;
    private BurbujaController bc;
    private int fuegosLength;
    void Awake() {
        bc = GetComponent<BurbujaController>();
        fuegosLength = GetComponent<BurbujaStats>().fuegoFatuos.Length - 1;
    }
    void OnTriggerEnter(Collider other){ 
        Debug.Log("Collided with something: " + other.tag);
        if (other.gameObject.tag == "Altar") {
            Debug.Log("Collided with Altar, fire count: " + bc.cont);
            Debug.Log("Fire length: " + fuegosLength);
            if(bc.cont == fuegosLength) {
                var inst = Instantiate(cernunnos);
                Instantiate(summonEffect, new Vector3(-29f, 2f, -53f), Quaternion.identity);
                Debug.Log("Has reunido todos los fuegos");
            }
        }
    }
}
