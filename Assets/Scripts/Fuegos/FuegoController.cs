using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoController : MonoBehaviour
{
    public Transform[] waypoints;
    Vector3 targetPoint;
    Vector3 pos;
    Rigidbody rb;
    public int indiceVector;
    public float speed = 1;
    float x;
    float y;
    float z;
    Transform tr;
    Transform burbujatr;
    GameObject burbuja;
    FuegoStats fs;

    //public float distanceThreshold;


    void Awake() {
        findWaypoints();
        tr = gameObject.GetComponent<Transform>();
        fs = gameObject.GetComponent<FuegoStats>();
        burbuja = GameObject.FindGameObjectWithTag("Bubble");
        //burbujatr = burbuja.GetComponent<Transform>();
        x = Random.Range(-2, 2);
        y = Random.Range(-2, 2);
        z = Random.Range(-2, 2);
    }
    void Start()
    {
        fs.libre = true;
        indiceVector = Random.Range(1, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(1, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        fs.teLloc = true;
        Teletransportarse();
    }

    void FixedUpdate() 
    {
        fireFloat();
        if (fs.libre) {
            if (fs.teLloc == false) {
                    changeTarget();
                }
            else {
                MoveMe();
            }
        }
        else{
            seguirBurbuja();
        }     
    }


    void fireFloat() {
        pos = transform.position;
        pos.y += Mathf.Sin (2 * Time.fixedTime * Mathf.PI * fs.frecuencia) * fs.amplitud;
        transform.position = pos;

        /*La fórmula de la onda del seno es y(t) = A * sin(2 * pi * f * t + fase)
            A mayor amplitud (A), mayor será altura de los picos
            A mayor frecuencia (f), más oscilaciones por unidad de tiempo*/
    }
    void findWaypoints() {
        waypoints = GameObject.Find("Waypoints").GetComponentsInChildren<Transform>();
    }
    void MoveMe(){
        transform.position = Vector3.Lerp (transform.position, targetPoint, Time.deltaTime * speed);
    }

    void Teletransportarse(){
        transform.position = targetPoint;
    }

    public void liberarSitio() {
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = false;
    }


    public void changeTarget(){
        indiceVector = Random.Range(1, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(1, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        fs.teLloc = true;
    }

    void seguirBurbuja(){
        tr.position = Vector3.MoveTowards (tr.position, burbujatr.position + new Vector3(x, y, z), Time.deltaTime * fs.speed);
    }
}
