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
        /* if (col.gameObject.tag == "Dagda")
        {
            //Teletransportar burbuja
            if (bubble == null) GameObject.FindGameObjectWithTag("Bubble").transform.position  = spawnPoint.transform.position;
            player.transform.position = spawnPoint.transform.position;
            return;
            //Instanciar un efecto de reaparición o algo
        }
        else if (col.gameObject.tag == "Bubble")
        {
            if (bubble == null) {
                bubble = GameObject.FindGameObjectWithTag("Bubble");
            }
            bubble.transform.position = spawnPoint.transform.position;

            return;
        } */
        if (col.gameObject.tag == "Dagda") playerStats.dead(); //Muere el jugador
        Destroy(col.gameObject);
    }
}
