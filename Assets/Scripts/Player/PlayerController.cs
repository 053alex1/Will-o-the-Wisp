using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public Animator playerAnimator;
    public GameObject GFX;
    public bool seg = false; //crec que no afecta en res
    GameObject player;
    public Vector3 move = new Vector3(0, 55, -55);
    public GameObject bubblePrefab;
    public GameObject burbuja;
    public playerStats ps;
    private Transform maincam;
    private Quaternion aux;
    private Vector3 forward = Vector3.zero;
    private Vector3 movement = Vector3.zero;
    private Shoot shootScript;
    private GameObject disparador;
    

    //Todos estos valores se moverán a otro script 
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        playerAnimator = GFX.transform.GetComponent<Animator>();
        maincam = Camera.main.transform;
        disparador = GameObject.Find("Disparador");
        shootScript = disparador.GetComponent<Shoot>();
    }

    void Start()
    {
        print("Let\'s get this bread.");
        tr = player.transform.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
        ps.isGrounded = true;
        ps.isRunning = false;
    }

    void Update()
    {
        Move();
        Jump();
        bubbleFunction();
        Attack();
    }

    void Move()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            ps.isRunning = true;
            ps.speed *= 2;
            playerAnimator.SetBool("isRunning", true);
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            ps.isRunning = false;
            ps.speed /= 2;
            playerAnimator.SetBool("isRunning", false);
        }
        
        forward = Vector3.Scale(maincam.forward, new Vector3(1,0,1)).normalized;    //Indica hacia dónde está el frente respecto a la cámara
        move = (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * maincam.right).normalized;
        move.x = move.x * Time.deltaTime * ps.speed;
        move.z = move.z * Time.deltaTime * ps.speed;
        move.y = rb.velocity.y;
        tr.Translate(move);

        tr.Find("Dagda").transform.rotation = Quaternion.Euler(0f, maincam.eulerAngles.y, 0f);
        disparador.transform.rotation = Quaternion.Euler(0f, maincam.eulerAngles.y, 0f);
        

        playerAnimator.SetFloat("Walking", Mathf.Abs(move.x + move.z));
        
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && ps.isGrounded)
        {
            rb.velocity += -1f * Physics.gravity.normalized * this.CalculateJumpVerticalSpeed(this.ps.jumpHeight);
            ps.isGrounded = false;
            playerAnimator.SetBool("isJumping", true);
        }
    }


    //!!!!!!!!!! ------------- CAMBIAR EL ATAQUE EN VEZ DE AL PUNTO DEL RATÓN, AL PUNTO DEL CROSSHAIR
    void LightAttack() {
        if (ps.mana >= 2) {
            Debug.Log("Light Attack triggered - Mana is " + ps.mana);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool check = Physics.Raycast(ray, out hit, Mathf.Infinity);
            shootScript.lightShoot();
            if (check)
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.red, 10f);
                Debug.Log("Light attack did Hit");

                ps.reduceMana(ps.lightDamage);
                
                if (hit.collider.tag == "Enemy") {
                    hit.collider.GetComponent<BaseEnemy>().getHit(ps.lightDamage);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.white, 10f);
                Debug.Log("Light attack did not Hit");
            }
        } else Debug.Log("Not enough mana - Mana is " + ps.mana);
    }

    void HeavyAttack() {
        if (ps.mana >= 5) {
            Debug.Log("Heavy Attack triggered - Mana is " + ps.mana);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool check = Physics.Raycast(ray, out hit, Mathf.Infinity);
            shootScript.heavyShoot();
            if (check)
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.red, 10f);
                Debug.Log("Heavy attack did Hit");

                ps.reduceMana(ps.heavyDamage);

                if (hit.collider.tag == "Enemy") {
                    hit.collider.GetComponent<BaseEnemy>().getHit(ps.heavyDamage);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.white, 10f);
                Debug.Log("Heavy attack did not Hit");
            }
        } else Debug.Log("Not enough mana - Mana is " + ps.mana);
    }

    void Attack() {
        playerAnimator.SetBool("isAttacking", true);
        if (Input.GetMouseButtonDown(0)) {
            LightAttack();
        }
        else if (Input.GetMouseButtonDown(1)) {
            HeavyAttack();
        }
        playerAnimator.SetBool("isAttacking", false);
    }

    float CalculateJumpVerticalSpeed(float height)
    {
        return Mathf.Sqrt(2f * height * Physics.gravity.magnitude);
    }

    void bubbleFunction()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            burbuja = GameObject.FindGameObjectWithTag("Bubble");
            if (burbuja != null) {
                Debug.Log("La burbuja está creada");
                if(Vector3.Distance(tr.position, burbuja.GetComponent<Transform>().position) < 16) {
                    Debug.Log("Burbuja Cerca");
                    if (burbuja.GetComponent<BurbujaStats>().seguir == true) {
                        Debug.Log("Dejando de seguir a Dagda");
                        burbuja.GetComponent<BurbujaStats>().seguir =false;
                    } else {
                        Debug.Log("Siguiendo a Dagda");
                        burbuja.GetComponent<BurbujaStats>().seguir =true;
                    }
                } else {
                    Debug.Log("Ya has creado una burbuja y estás muy lejos de ella para que te siga");
                    //Hacer algo para que el jugador se dé cuenta
                }
            } else {
                Instantiate(bubblePrefab, getBubblePosition(), Quaternion.identity);
                Debug.Log("La burbuja no estaba creada");
            }
        }
    }

    Vector3 getPlayerPosition()
    {
        return player.transform.position;
    }

    Vector3 getBubblePosition()
    {
        return getPlayerPosition() + new Vector3(0, 15, 0); //Falta por determinar la altura de Dagda
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            ps.isGrounded = true;
            playerAnimator.SetBool("isJumping", false);
        }
        
        if(collision.collider.tag == "Enemy")
        {
            Debug.Log("Has sido dañado");
            playerAnimator.SetBool("isHurting", true);
            ps.reduceHp(20.0f);
            playerAnimator.SetBool("isHurting", false);
            Debug.Log("Ya no eres dañado");

        }
    }
}
