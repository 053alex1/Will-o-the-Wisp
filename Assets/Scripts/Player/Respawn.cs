using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject player;
    private GameObject spawnPoint;
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Dagda");
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
    }


    void OnTriggerEnter(Collider col) {
        Debug.Log("Something collided");
        player.transform.position = spawnPoint.transform.position;
        //Instanciar un efecto de reaparición o algo
    }
}
