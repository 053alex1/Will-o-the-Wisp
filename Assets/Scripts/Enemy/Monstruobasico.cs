﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monstruobasico : MonoBehaviour
{
    //Radio en el que busca una nueva posicion aleatoria a la que ir en Wander
    public float RadioMov = 40;
    //Tiempo que tarda en buscar una nueva posicion a la que ir en Wander
    public float wanderTimer = 4;
    public float TimerCDAttack = 2;
    //Radio(distancia) maxima a la que busca a dagda
    public float RadioVision = 80;
    public float RadioSentido = 30;
    //Angulo de vision de busqueda de dagda
    public float fov = 90f;
    public float disAtqDis = 20;
    public Animator playerAnimator;
    private Animator myAnimator;
    public GameObject GFX;
    //Usados para el calculo(igual luego los quito de aquí
    protected Transform target;
    protected NavMeshAgent agent;
    private float timer, timerAttack;
 
    private enum Estados { ATACA_DIS, ATACA, CAMINANDO, SIGUIENDO };
    private Estados MaqEstados;
    //   private MetodosGenerales m;
    private void Awake()
    {
        MaqEstados = Estados.CAMINANDO;
        playerAnimator = GFX.transform.GetComponent<Animator>();
        myAnimator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Dagda").transform;
        timer = wanderTimer;
        timerAttack = TimerCDAttack;
        agent.speed = 40f;
        agent.acceleration = 18;
        
    //    agent.stoppingDistance = 10;
    }

    void Update()
    {
        timerAttack += Time.deltaTime;
        //logica sencilla: si no estas siguiendo al protagonista tu recorrido es aleatorio

        switch (MaqEstados) {
            case (Estados.CAMINANDO):
                {
                    wander();
                    MaqEstados = Estados.SIGUIENDO;
                    break;
                }
            case (Estados.SIGUIENDO):
                {
                    MaqEstados = Estados.CAMINANDO;
                    seguir();
                    break;
                }
            case (Estados.ATACA):
                {
                    ataca();
                    MaqEstados = Estados.SIGUIENDO;
                    break;
                }
            case (Estados.ATACA_DIS):
                {
                    MaqEstados = Estados.SIGUIENDO;
                    break;
                }
        };
       
    }
    private void ataca()
    {   
        if (timerAttack > TimerCDAttack)
        {
            myAnimator.SetBool("isWalking", false);
            timerAttack = 0;

            //ANIMACIÓN DE ATAQUE setTrigger
            myAnimator.SetTrigger("isAttacking");
            Debug.Log("Soy " + gameObject.name + " y he desactivado el walking y he hecho un ataque");
            playerStats targetStats = target.GetComponent<playerStats>();
            targetStats.getHit(1f);
        }
        myAnimator.SetBool("isWalking", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioVision);
    }
    private void seguir()
    {
        //Animación de caminar
        myAnimator.SetBool("isWalking", true);
        float dis, dot, dotfov;
        bool follow = false;
        //funciones para calcular si dagda esta en tu rango de vision y a tu distancia maxima de vision 
        Vector3 v = target.position - transform.position;
        dis = Mathf.Sqrt(v.sqrMagnitude);
        v.Normalize();
        dot = Vector3.Dot(transform.forward, v);
        dotfov = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);

        Debug.DrawRay(transform.position + transform.up, v * dis, Color.red);
        //Mirar
        if (dis < RadioVision && dot >= dotfov)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, v, out hit))
            {
                if (hit.collider.gameObject.tag == "Dagda")
                {
                    MaqEstados = Estados.SIGUIENDO;
                    follow = true;
                    agent.SetDestination(target.position);
                }
            }
        }
        //Sentir
        if (!follow && dis < RadioSentido)
        {
            MaqEstados = Estados.SIGUIENDO;
            follow = true;
            agent.SetDestination(target.position);

        }
        //atacar+
        if (follow && dis <= disAtqDis)
        {
            if (dis <= agent.stoppingDistance)
            {
                MaqEstados = Estados.ATACA;
                //Mirar al enemigo
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(v.x, 0, v.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            else MaqEstados = Estados.ATACA_DIS;
        }
    }

    public void wander()
    {
        //Animación de caminar setBool
        timer += Time.deltaTime;
        // Cada vez que el timer supera el wander time busca una nueva posicion aleatoria a la que ir
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, RadioMov, -1);
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