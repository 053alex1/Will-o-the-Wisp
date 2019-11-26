using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaController : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 posMov;
    public float distance;
    public float pushStrength;
    private Rigidbody rb;
    private GameObject bubble;
    private GameObject dagda;
    private GameObject padreFuegos;
    public GameObject[] fuegosLista;
    private Transform tr;
    private BurbujaStats bs;
    private Transform fuego;
    private FuegoStats fs;
    private Transform dagtr;
    private Transform[] fuegos;
    float radius = 3f;
    public int cont;
    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("Bubble");
        dagda = GameObject.FindGameObjectWithTag("Dagda");
        padreFuegos = GameObject.Find("Fuegos");
        dagtr= dagda.GetComponent<Transform>();
        //rb = bubble.GetComponent<Rigidbody>();
        tr = bubble.GetComponent<Transform>();
        bs = bubble.GetComponent<BurbujaStats>();
        fuegosLista = GameObject.FindGameObjectsWithTag("Fuego");
        tr.position = dagtr.position  + new Vector3(15, 0, 0);
        cont = 0;
    }

    void FixedUpdate()
    {
        bubbleFloat();
        fuegosCerca3();

        if (bs.resistencia <= 0) romperBurbuja3();
        
        if (bs.seguir) {
            seguirProta();
            bs.quet = false;
        }
        else {
            pararSeguirProta();
        }
        tr.Rotate(0.0f, 360.0f / 5.0f * Time.deltaTime, 0.0f);
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
    
    void romperBurbuja3() {
        cont = 0;
        tr.DetachChildren();
        foreach (GameObject fuego in fuegosLista)
        {
            fuego.GetComponent<FuegoStats>().libre = true;
        }
        Destroy(gameObject);
    }

    void fuegosCerca3() {
        foreach (GameObject fuego in fuegosLista)
        {
            
            if (Vector3.Distance(tr.position, fuego.GetComponent<Transform>().position) < 5 && fuego.GetComponent<FuegoStats>().libre) {
                meterFuego(fuego.GetComponent<Transform>());
            }
        }
    }

    void meterFuego(Transform fuego) {
        fuego.GetComponent<FuegoStats>().libre = false;
        fuego.GetComponent<FuegoStats>().teLloc = false;
        fuego.GetComponent<FuegoController>().liberarSitio();
        cont++;
        //fuego.position = Vector3.MoveTowards (fuego.position, tr.position, Time.deltaTime * bs.speed);
        recalcularPos();
        fuego.parent = tr;
    }

    public void transmisionInstantanea() {
        tr.position = dagtr.position  + new Vector3(15, 0, 0);
    }
    void seguirProta(){
        
        //float angle = Vector3.Angle(targetDir, tr.forward);
        //tr.position.Angle(targetDir, tr.forward) = 4.0f;

    //     var dist = Vector3.Distance(tr.position, dagtr.position);

    //     if (dist < distance)
    //  {
    //      //Calculate the vector between the object and the player
    //     Vector3 targetDir = dagtr.position - tr.position;
    //      //Cancel out the vertical difference
    //      targetDir.y = 0;
    //      //Translate the object in the direction of the vector
    //      tr.Translate(targetDir.normalized * pushStrength);
    //  }


        //tr.position = Vector3.MoveTowards (tr.position, dagtr.forward, Time.deltaTime * bs.speed);
        //tr.position = Vector3.MoveTowards (tr.position, dagtr.forward, Time.deltaTime * bs.speed);
    
        tr.position = Vector3.MoveTowards (tr.position, dagtr.position + new Vector3(7, 5, 0), Time.deltaTime * bs.speed);
    }
    void pararSeguirProta(){
        tr.position = Vector3.MoveTowards (tr.position, tr.position, Time.deltaTime * bs.speed);
    }

    void recalcularPos(){
        int contador = 0;
        foreach (GameObject fuego in fuegosLista)
        {
            if (fuego.GetComponent<FuegoStats>().libre == false) {
                float angle = contador * (Mathf.PI*2 / cont);
                Vector3 posic = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * bs.radio/2;
                fuego.GetComponent<Transform>().position = Vector3.MoveTowards (fuego.GetComponent<Transform>().position, tr.position, Time.deltaTime * bs.speed);
                fuego.GetComponent<Transform>().position = Vector3.MoveTowards (tr.position, posic + tr.position, Time.deltaTime * bs.speed);
                contador++;
            }
        }
    }

}
