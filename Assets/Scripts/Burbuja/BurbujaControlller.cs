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
    void Update()
    {
        bubbleFloat();
    }

    void bubbleFloat() {
        pos = tr.position;
        pos.y += Mathf.Sin (2 * Time.fixedTime * Mathf.PI * bs.frecuencia) * bs.amplitud;
        tr.position = pos;

        /*La fórmula de la onda del seno es y(t) = A * sin(2 * pi * f * t + fase)
            A mayor amplitud (A), mayor será altura de los picos
            A mayor frecuencia (f), más oscilaciones por unidad de tiempo*/
    }
}
