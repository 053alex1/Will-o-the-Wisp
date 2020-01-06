using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonstruoFuath : MonoBehaviour
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
    public GameObject burbuja;
    //Usados para el calculo(igual luego los quito de aquí
    protected Transform target;
    protected NavMeshAgent agent;
    private float timer, timerAttack, obj;
    private Vector3 posAntigua;


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
        burbuja = GameObject.FindGameObjectWithTag("Bubble");
        obj = 0;

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
                    float dis = float.MaxValue;
                    MaqEstados = Estados.CAMINANDO;
                    burbuja = GameObject.FindGameObjectWithTag("Bubble");
                    //if (burbuja)
                    //    dis = seguir(burbuja.transform,float.MaxValue);
                    obj = seguir(target, dis);

                    break;
                }
            case (Estados.ATACA):
                {
                    
                    ataca(obj);
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
    private void ataca(float obj)
    {
        if (timerAttack > TimerCDAttack)
        {
            myAnimator.SetBool("isWalking", false);
            timerAttack = 0;

            //ANIMACIÓN DE ATAQUE setTrigger
            myAnimator.SetTrigger("isAttacking");
            // Debug.Log("Soy " + gameObject.name + " y he desactivado el walking y he hecho un ataque");
            if (obj == -1)
            {
                BurbujaStats b = burbuja.transform.GetComponent<BurbujaStats>();
                b.dañoRecibido();
            }
            else
            {
                playerStats targetStats = target.GetComponent<playerStats>();
                targetStats.getHit(1f);
            }
        }
        myAnimator.SetBool("isWalking", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioVision);
    }
    private float seguir(Transform target,float d)
    {
        
        
        float dis, dot, dotfov;
        bool follow = false;
        //funciones para calcular si dagda esta en tu rango de vision y a tu distancia maxima de vision 
        Vector3 v = target.position - transform.position;
        dis = Mathf.Sqrt(v.sqrMagnitude);
        if (dis <= d)
        {
            //Animación de caminar
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
                    if (hit.collider.gameObject.tag == "Dagda" || hit.collider.gameObject.tag == "Bubble")
                    {

                        myAnimator.SetBool("isWalking", true);
                        MaqEstados = Estados.SIGUIENDO;
                        follow = true;
                        agent.SetDestination(target.position);
                    }
                }
            }
            //Sentir
            if (!follow && dis < RadioSentido)
            {

                myAnimator.SetBool("isWalking", true);
                MaqEstados = Estados.SIGUIENDO;
                follow = true;
                agent.SetDestination(target.position);

            }
            //atacar+


            if (follow && dis <= disAtqDis)
            {
                if (dis <= agent.stoppingDistance)
                {
                    myAnimator.SetBool("isWalking", false);
                    MaqEstados = Estados.ATACA;
                    //Mirar al enemigo
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(v.x, 0, v.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }

                else MaqEstados = Estados.ATACA_DIS;
            }
            return dis;
        }
        else return -1;
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
            posAntigua = newPos;
            timer = 0;
            myAnimator.SetBool("isWalking", true);
        }
         
        else if (posAntigua.x == transform.position.x && posAntigua.z == transform.position.z)
        {
            //Debug.Log("Soy " + gameObject.name);
            myAnimator.SetBool("isWalking", false);
        }
        //Debug.Log(transform.position.x + transform.position.z + "Tengo q ir a"+ posAntigua  );
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