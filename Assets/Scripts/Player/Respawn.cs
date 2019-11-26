using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject player;
    private playerStats playerStats;
    private GameObject bubble;

    private GameObject spawnPoint;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        playerStats = player.GetComponent<playerStats>();
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
        bubble = GameObject.FindGameObjectWithTag("Bubble");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Something collided: " + col.gameObject.name);
        if (col.gameObject.name == "Dagda") playerStats.dead(); //Muere el jugador
        col.gameObject.SetActive(false);
    }
}
