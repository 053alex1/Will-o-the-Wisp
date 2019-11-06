using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaControlller : MonoBehaviour
{
    Vector3 pos;
    Rigidbody rb;
    GameObject bubble;
    Transform tr;
    BurbujaStats bs;

    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("Bubble");
        rb = bubble.GetComponent<Rigidbody>();
        tr = bubble.GetComponent<Transform>();
        bs = bubble.GetComponent<BurbujaStats>();
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
        for(int i = 0; i < bs.fuegoFatuos.Length; i++){
            Transform fuego = bs.fuegoFatuos[i];
            if (fuego.GetComponent<FuegoController>().libre == false) {
                fuego.GetComponent<FuegoController>().libre = true;
                fuego.parent = null;
                //fuego.GetComponent<FuegoController>().changeTarget();
                bs.cont--;
            }
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {   
        //Vector3.Distance(transform.position, otherObject.transform.position) -- cada frame
        if (other.gameObject.tag == "Fuego") {
            Transform fuego = other.transform;
            fuego.GetComponent<FuegoController>().libre = false;
            fuego.parent = tr;
            bs.cont ++;
        }

        if (other.gameObject.tag == "Altar" && bs.cont == bs.fuegoFatuos.Length) {
            Debug.Log("Has reunido todos los fuegos YEEEAA");
        }
    }

    
}
