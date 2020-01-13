using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject cernunnos;
    public GameObject summonEffect;
    private BurbujaController bc;
    private int fuegosLength;

    public GameObject donn;
    
    private GameObject dullahan;
    private GameObject bubble;

    void Awake() {
        bc = GetComponent<BurbujaController>();
        fuegosLength = GetComponent<BurbujaStats>().fuegoFatuos.Length - 1;
        bubble = GameObject.Find("Burbuja(Clone)");
    }
    void Update()
    {
        comprobarSiMuerto();
    }

    
    private void comprobarSiMuerto()
    {
        dullahan = GameObject.Find("Dullahan");
        if(dullahan == null)
        {/*
            Invoke("DonnSummon", 6.4f);
            GameObject  effect = Instantiate(summonEffect, new Vector3(-29f, 2f, -53f), Quaternion.identity) as GameObject;
            ParticleSystem part = effect.GetComponent<ParticleSystem>();
            Destroy(effect, part.main.duration);
            */
            DonnSummon();
            Debug.Log("Has reunido todos los fuegos");
            DestroyBubbleAndFires();
        }
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

    void DonnSummon() {
        Instantiate(donn);
    }

    void DestroyBubbleAndFires()
    {
        bc.gui.DestroyEnergyBar();
        bubble.SetActive(false);
    }
}
