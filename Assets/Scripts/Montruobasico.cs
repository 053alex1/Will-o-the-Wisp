using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Montruobasico : MonoBehaviour
{ 
public float wanderRadius;
public float wanderTimer;
public float radius ;
public float fov = 30f;
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
        if(!follow)
            wander();
        seguir();
    
}
    public void seguir()
    {
        float dis;
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

    NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

    return navHit.position;
}
}
