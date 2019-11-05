using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monstruobasico : MetodosGenerales
{ 
//Radio en el que busca una nueva posicion aleatoria a la que ir en Wander
public float wanderRadius=40;
//Tiempo que tarda en buscar una nueva posicion a la que ir en Wander
public float wanderTimer=4;
//Radio(distancia) maxima a la que busca a dagda
public float radius=50 ;
//Angulo de vision de busqueda de dagda
public float fov = 90f;

//Usados para el calculo(igual luego los quito de aquí)
private float dot = 0;

protected Transform target;
protected NavMeshAgent agent;
private float timer;
private bool follow= false;
 //   private MetodosGenerales m;

    // Use this for initialization
    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    target = GameObject.FindWithTag("Dagda").transform;
        timer = wanderTimer;
        agent.speed = 50f;
        agent.acceleration = 20;
        agent.stoppingDistance = 1;

}

// Update is called once per frame
void Update()
{       
        //logica sencilla: si no estas siguiendo al protagonista tu recorrido es aleatorio
        if(!follow)
            wander( agent,  timer,  wanderTimer,  wanderRadius);
        follow=seguir( agent,  target,  fov,  radius);
    
}


}
