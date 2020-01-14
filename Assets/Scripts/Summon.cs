using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Summon : MonoBehaviour
{
    private BurbujaController bc;
    private int fuegosLength;
    [SerializeField]
    private GameObject cernunnos;
    [SerializeField]
    private GameObject cernunnosEffect;
    [SerializeField]
    private GameObject dullahan;
    [SerializeField]
    private GameObject dullahanEffect;
    [SerializeField]
    private GameObject donn;
    [SerializeField]
    private GameObject donnEffect;
    private GameObject dullahanPer;
    private GameObject bubble;
    public bool comprobar = false;
    public bool stopComprobar = false;

    void Awake() {
        bc = GetComponent<BurbujaController>();
        fuegosLength = GetComponent<BurbujaStats>().fuegoFatuos.Length - 1;
        bubble = GameObject.Find("Burbuja(Clone)");
    }
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
        dullahanPer = GameObject.Find("Dullahan(Clone)");
        if(dullahanPer == null)
        {
            DonnSummon();
            stopComprobar=true;
        }
    }
    
    void OnTriggerEnter(Collider other){ 
        Debug.Log("Collided with something: " + other.tag);
        if (other.gameObject.tag == "Altar") {
            Debug.Log("Collided with Altar, fire count: " + bc.cont);
            Debug.Log("Fire length: " + fuegosLength);
            if(bc.cont == fuegosLength) {
                if(SceneManager.GetActiveScene().buildIndex == 4)
                {
                    Instantiate(dullahan, new Vector3(891, 23, 90), Quaternion.identity);
                    GameObject  effect = Instantiate(dullahanEffect, new Vector3(885, 10, 95), Quaternion.identity) as GameObject;
                    ParticleSystem part = effect.GetComponent<ParticleSystem>();
                    Destroy(effect, part.main.duration);
                    comprobar = true;
                } else {
                    Invoke("CernunnosSummon", 6.4f);
                    GameObject  effect = Instantiate(cernunnosEffect, new Vector3(-29f, 2f, -53f), Quaternion.identity) as GameObject;
                    ParticleSystem part = effect.GetComponent<ParticleSystem>();
                    Destroy(effect, part.main.duration);
                }
                
                Debug.Log("Has reunido todos los fuegos");
                DestroyBubbleAndFires();
            }
        }
    }

    void CernunnosSummon() {
        Instantiate(cernunnos);
    }

    void DonnSummon() {
        Instantiate(donn, new Vector3(885, 25, 95), Quaternion.identity);
    }

    void DestroyBubbleAndFires()
    {
        bc.gui.DestroyEnergyBar();
        bubble.SetActive(false);
    }
}
