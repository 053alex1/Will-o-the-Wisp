using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoController : MonoBehaviour
{
    public Transform[] waypoints;
    Vector3 targetPoint;
    public int indiceVector;
    public float speed = 1;
    public bool libre = true;
    public bool teLloc = false;
    //public float distanceThreshold;


    void Awake() {
        findWaypoints();
    }
    void Start()
    {
        indiceVector = Random.Range(0, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(0, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        teLloc = true;
        Teletransportarse();
    }

    void Update() 
    {
        if (libre) {
            if (teLloc == false) {
                    changeTarget();
                }
            else {
                MoveMe();
            }
        }      
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
        indiceVector = Random.Range(0, waypoints.Length);
        while(waypoints[indiceVector].GetComponent<WayPoint>().ocupado == true){
            indiceVector = Random.Range(0, waypoints.Length);
        }
        targetPoint = waypoints[indiceVector].position;
        waypoints[indiceVector].GetComponent<WayPoint>().ocupado = true;
        teLloc = true;
        MoveMe();
    }
}
