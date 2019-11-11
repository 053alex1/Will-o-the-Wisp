using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaController : MonoBehaviour
{
    private Vector3 pos;
    private Rigidbody rb;
    private GameObject bubble;
    private GameObject dagda;
    private GameObject padreFuegos;
    private Transform tr;
    private BurbujaStats bs;
    private Transform fuego;
    private FuegoStats fs;
    private Transform dagtr;
    private Transform[] fuegos;

    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("Bubble");
        dagda = GameObject.FindGameObjectWithTag("Dagda");
        padreFuegos = GameObject.Find("Fuegos");
        dagtr= dagda.GetComponent<Transform>();
        rb = bubble.GetComponent<Rigidbody>();
        tr = bubble.GetComponent<Transform>();
        bs = bubble.GetComponent<BurbujaStats>();
        findFires();
    }
    void FixedUpdate()
    {
        bubbleFloat();
        fuegosCerca();

        if (bs.resistencia <= 0) romperBurbuja2();
        
        if (bs.seguir) seguirProta();
        else pararSeguirProta();
    }

    void findFires() {
        bs.fuegoFatuos = padreFuegos.GetComponentsInChildren<Transform>();
    }
    void bubbleFloat() {
        pos = tr.position;
        pos.y += Mathf.Sin (2 * Time.fixedTime * Mathf.PI * bs.frecuencia) * bs.amplitud;
        tr.position = pos;
        /*  La fórmula de la onda del seno es y(t) = A * sin(2 * pi * f * t + fase)
            A mayor amplitud (A), mayor será altura de los picos
            A mayor frecuencia (f), más oscilaciones por unidad de tiempo
        */
    }
    void romperBurbuja2() {
        foreach (Transform fireChild in tr.GetComponentsInChildren<Transform>()) {
            if (fireChild.gameObject.name != "Burbuja(Clone)") {
                Debug.Log("Fire name: " + fireChild.gameObject.name);
                fireChild.transform.SetParent(padreFuegos.transform);
                fireChild.GetComponent<FuegoStats>().libre = true;
            }
        }

        tr.DetachChildren();
        Destroy(gameObject);
    }
    void romperBurbuja() {
        //tr.DetachChildren();
        //for(int i = 0; i <= padreFuegos.transform.childCount; i++){
        for(int i = 1; i <= bs.fuegoFatuos.Length; i++){
        //Debug.Log("Deuria entrar");
            //fuego = padreFuegos.transform.GetChild(i);
            fuego = bs.fuegoFatuos[i];
        //if(fuego != null) {
        //    Debug.Log("El foc existeix");
        //}
        //Transform fuego1 = padreFuegos.transform.GetChild(1);
        //Transform fuego2 = padreFuegos.transform.GetChild(2);
        //Transform fuego3 = padreFuegos.transform.GetChild(3);
        //Transform fuego4 = padreFuegos.transform.GetChild(4);
            fs = fuego.GetComponent<FuegoStats>();
            fs.libre = true;
        //if(fuego.GetComponent<FuegoStats>().libre == true) {
        //    Debug.Log("Fuego Liberado");
        //}
        //fuego1.GetComponent<FuegoStats>().libre = true;
        //fuego2.GetComponent<FuegoStats>().libre = true;
        //fuego3.GetComponent<FuegoStats>().libre = true;
        //fuego4.GetComponent<FuegoStats>().libre = true;
            //fuego.parent = null;
            //fuego.parent = padreFuegos.GetComponent<Transform>();
            //fuego.GetComponent<FuegoController>().changeTarget();
        }
        Destroy(gameObject);
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
        fuego.transform.SetParent(bubble.transform);
        fuego.GetComponent<FuegoStats>().libre = false;
        fuego.GetComponent<FuegoStats>().teLloc = false;
        //fuego.GetComponent<FuegoController>().waypoints[fuego.GetComponent<FuegoController>().indiceVector].GetComponent<WayPoint>().ocupado = false;
        fuego.GetComponent<FuegoController>().liberarSitio();
        //fuego.parent = tr;
    }
    void escaparFuego(Transform fuego) {
        fuego.GetComponent<FuegoStats>().libre = true;
        fuego.GetComponent<FuegoStats>().teLloc = true;
    }
    void OnTriggerEnter(Collider other){ 
        //if (other.gameObject.tag == "Fuego") {
          //  other.gameObject.transform.GetComponent<FuegoStats>().libre = false;
            //other.gameObject.transform.parent = tr;
        //}

        if (other.gameObject.tag == "Altar") {
            int cont = 0;
            for(int i = 1; i < bs.fuegoFatuos.Length; i++){
                Transform fuego = bs.fuegoFatuos[i];
                if (fuego.GetComponent<FuegoStats>().libre == false) {
                   cont++;
                }
            }
            if(cont == bs.fuegoFatuos.Length - 1) {
                Debug.Log("Has reunido todos los fuegos YEEEAA");
            }
        }
    }
    void seguirProta(){
        tr.position = Vector3.MoveTowards (tr.position, dagtr.position + new Vector3(0, 15, 0), Time.deltaTime * bs.speed);
    }
    void pararSeguirProta(){
        tr.position = Vector3.MoveTowards (tr.position, tr.position, Time.deltaTime * bs.speed);
    }
}
