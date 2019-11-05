using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monstruobasico : MonoBehaviour
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
private float dotfov;


private Transform target;
private NavMeshAgent agent;
private float timer;
private bool follow= false;

    // Use this for initialization
    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    target = GameObject.FindWithTag("Dagda").transform;
        timer = wanderTimer;

}

// Update is called once per frame
void Update()
{       
        //logica sencilla: si no estas siguiendo al protagonista tu recorrido es aleatorio
        if(!follow)
            wander();
        seguir();
    
}
    public void seguir()
    {
        float dis;
        //funciones para calcular si dagda esta en tu rango de vision y a tu distancia maxima de vision 
        Vector3 v = target.position - transform.position;
        dis = v.sqrMagnitude;
        v.Normalize();
        dot = Vector3.Dot(transform.forward, v);
        dotfov = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);
        
        if (dis < radius*radius && dot>=dotfov)
        {
        
            agent.SetDestination(target.position);
            follow = true;
        }
        else
        {
            follow = false;
        }
    }

    public void wander()
    {
        
        timer += Time.deltaTime;
        // Cada vez que el timer supera el wander time busca una nueva posicion aleatoria a la que ir
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
{
    Vector3 randDirection = Random.insideUnitSphere * dist;
    
    randDirection += origin;

    NavMeshHit navHit;
    //Hace q la posicion aleatoria buscada sea una superficie valida
    NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

    return navHit.position;
}
}
