using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject player;
    private GameObject bubble;

    private GameObject spawnPoint;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        spawnPoint = GameObject.FindGameObjectWithTag("spawnPoint");
        bubble = GameObject.FindGameObjectWithTag("Bubble");
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Something collided");
        if (col.gameObject.tag == "Dagda")
        {
            //Teletransportar burbuja
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
        }

        Destroy(col.gameObject);

    }
}
