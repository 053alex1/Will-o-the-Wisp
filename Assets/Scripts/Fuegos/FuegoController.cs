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
    public bool libre = true;
    public bool teLloc = false;
    Transform tr;
    FuegoStats fs;

    //public float distanceThreshold;


    void Awake() {
        findWaypoints();
        fs = gameObject.GetComponent<FuegoStats>();
    }
    void Start()
    {
        indiceVector = Random.Range(1, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(1, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        teLloc = true;
        Teletransportarse();
    }

    void FixedUpdate() 
    {
        fireFloat();
        if (libre) {
            if (teLloc == false) {
                    changeTarget();
                }
            else {
                MoveMe();
            }
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


    public void changeTarget(){
        indiceVector = Random.Range(1, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(1, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        teLloc = true;
        MoveMe();
    }
}
