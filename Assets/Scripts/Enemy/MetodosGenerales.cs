using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MetodosGenerales : MonoBehaviour
{
    
    public bool seguir(NavMeshAgent agent, Transform target, float fov, float radius)
    {
        float dis, dot, dotfov;
        //funciones para calcular si dagda esta en tu rango de vision y a tu distancia maxima de vision 
        Vector3 v = target.position - transform.position;
        dis = v.sqrMagnitude;
        v.Normalize();
        dot = Vector3.Dot(transform.forward, v);
        dotfov = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);

        if (dis < radius * radius && dot >= dotfov)
        {

            agent.SetDestination(target.position);
            return true;
        }
        else
        {
            return false;
        }
    }


    public void wander(NavMeshAgent agent, float timer, float wanderTimer, float wanderRadius)
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
