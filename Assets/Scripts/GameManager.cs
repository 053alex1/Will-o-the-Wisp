using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance = null;
    public GameObject playerObject = null;
    public PlayerController playerController;
    public GameObject burbujaObject = null;
    public BurbujaController burbujaController;
    void Awake()
    {
        singleton();
        getPlayer();
        getBurbuja();
    }

    void singleton() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null) Destroy(gameObject);
    }
    public GameObject getPlayer()
    {
        if (playerObject == null)
            playerObject = GameObject.FindGameObjectWithTag("Dagda");

        return playerObject;
    }
    public PlayerController getPlayerController()
    {
        if (playerController == null)
            playerController = getPlayer().GetComponent<PlayerController>();

        return playerController;
    }
    public GameObject getBurbuja()
    {
        if (burbujaObject == null)
            burbujaObject = GameObject.FindGameObjectWithTag("Bubble");

        return burbujaObject;
    }
    public BurbujaController getBurbujaController()
    {
        if (burbujaController == null)
            burbujaController = getBurbuja().GetComponent<BurbujaController>();

        return burbujaController;
    }
}
