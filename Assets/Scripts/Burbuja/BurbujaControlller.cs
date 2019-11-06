using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaControlller : MonoBehaviour
{
    Vector3 pos;
    Rigidbody rb;
    GameObject bubble;
    
    GameObject dagda;
    Transform tr;
    BurbujaStats bs;

    Transform dagtr;


    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("Bubble");
        dagda = GameObject.FindGameObjectWithTag("Dagda");
        dagtr= dagda.GetComponent<Transform>();
        rb = bubble.GetComponent<Rigidbody>();
        tr = bubble.GetComponent<Transform>();
        bs = bubble.GetComponent<BurbujaStats>();
        findFires();
    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        bubbleFloat();
        if (bs.resistencia <= 0) {
            romperBurbuja();
        }
        // fuegosCerca();
        if (bs.seguir){
            seguirProta();
        }else{
            pararSeguirProta();
        }
    }

    void findFires() {
        bs.fuegoFatuos = GameObject.Find("Fuegos").GetComponentsInChildren<Transform>();
    }

    void bubbleFloat() {
        pos = tr.position;
        pos.y += Mathf.Sin (2 * Time.fixedTime * Mathf.PI * bs.frecuencia) * bs.amplitud;
        tr.position = pos;

        /*La fórmula de la onda del seno es y(t) = A * sin(2 * pi * f * t + fase)
            A mayor amplitud (A), mayor será altura de los picos
            A mayor frecuencia (f), más oscilaciones por unidad de tiempo*/
    }

    void romperBurbuja() {
        tr.DetachChildren();
        for(int i = 1; i < bs.fuegoFatuos.Length; i++){
            Debug.Log("Pasando por el fuego");

            Transform fuego = bs.fuegoFatuos[i];
            if (fuego.GetComponent<FuegoController>().libre == false) {
                Debug.Log("Este fuego no esta libre");
                fuego.GetComponent<FuegoController>().libre = true;
                fuego.GetComponent<FuegoController>().teLloc = false;
                // fuego.parent = null;
                fuego.GetComponent<FuegoController>().changeTarget();
            }
        }
        Destroy(bubble);
    }


    void fuegosCerca() {
        for(int i = 1; i < bs.fuegoFatuos.Length; i++){
            Transform fuego = bs.fuegoFatuos[i];
            if (Vector3.Distance(tr.position, fuego.position) < 5) {
                meterFuego(fuego);
            }
        }
    }
        //Vector3.Distance(transform.position, otherObject.transform.position) -- cada frame
    void meterFuego(Transform fuego) {
            fuego.GetComponent<FuegoController>().libre = false;
            fuego.parent = tr;
        }

    void OnTriggerEnter(Collider other){ 
        if (other.gameObject.tag == "Fuego") {
            other.gameObject.transform.GetComponent<FuegoController>().libre = false;
            other.gameObject.transform.parent = tr;
        }

        if (other.gameObject.tag == "Altar") {
            int cont = 0;
            for(int i = 1; i < bs.fuegoFatuos.Length; i++){
                Transform fuego = bs.fuegoFatuos[i];
                if (fuego.GetComponent<FuegoController>().libre == false) {
                   cont++;
                }
            }
            if(cont == bs.fuegoFatuos.Length - 1) {
                Debug.Log("Has reunido todos los fuegos YEEEAA");
            }
        }
    }
    void seguirProta(){
        tr.position = Vector3.Lerp (tr.position, dagtr.position + new Vector3(0, 15, 0), Time.deltaTime * bs.speed);
    }
    void pararSeguirProta(){

    }
}
