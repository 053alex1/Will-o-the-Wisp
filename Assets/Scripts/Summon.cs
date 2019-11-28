using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject cernunnos;
    public GameObject summonEffect;
    private BurbujaController bc;
    private int fuegosLength;

    private GameObject bubble;

    void Awake() {
        bc = GetComponent<BurbujaController>();
        fuegosLength = GetComponent<BurbujaStats>().fuegoFatuos.Length - 1;
        bubble = GameObject.Find("Burbuja(Clone)");
    }
    void OnTriggerEnter(Collider other){ 
        Debug.Log("Collided with something: " + other.tag);
        if (other.gameObject.tag == "Altar") {
            Debug.Log("Collided with Altar, fire count: " + bc.cont);
            Debug.Log("Fire length: " + fuegosLength);
            if(bc.cont == fuegosLength) {
                Invoke("CernunnosSummon", 6.4f);
                GameObject  effect = Instantiate(summonEffect, new Vector3(-29f, 2f, -53f), Quaternion.identity) as GameObject;
                ParticleSystem part = effect.GetComponent<ParticleSystem>();
                Destroy(effect, part.main.duration);
                Debug.Log("Has reunido todos los fuegos");
                DestroyBubbleAndFires();
            }
        }
    }

    void CernunnosSummon() {
        Instantiate(cernunnos);
    }

    void DestroyBubbleAndFires()
    {
        bc.gui.DestroyEnergyBar();
        bubble.SetActive(false);
    }
}
