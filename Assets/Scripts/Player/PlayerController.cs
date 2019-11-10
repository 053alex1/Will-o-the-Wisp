using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public Animator playerAnimator;
    public GameObject GFX;
    public bool seg = false;

    GameObject player;
    public Vector3 move = new Vector3(0, 55, -55);
    public GameObject bubble;
    public GameObject burbuja;
    public playerStats ps;
    private Transform maincam;
    private Quaternion aux;
    private Vector3 forward = Vector3.zero;
    private Vector3 movement = Vector3.zero;
    

    //Todos estos valores se moverán a otro script 
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        playerAnimator = GFX.transform.GetComponent<Animator>();
        maincam = Camera.main.transform;
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
        createBubble();
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
        
        forward = Vector3.Scale(maincam.forward, new Vector3(1,0,1)).normalized;
        move = (Input.GetAxis("Vertical") * forward * Time.deltaTime + Input.GetAxis("Horizontal") * maincam.right * Time.deltaTime).normalized;
        move.y = rb.velocity.y;
        Debug.Log("move is " + move);
        //move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * ps.speed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * ps.speed);
        tr.Translate(move);
        /* aux = maincam.rotation;
        aux.x = 0;
        aux.z = 0;
        tr.rotation = aux; */

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

    void LightAttack() {
        if (ps.mana >= 2) {
            Debug.Log("Light Attack triggered - Mana is " + ps.mana);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool check = Physics.Raycast(ray, out hit, Mathf.Infinity);

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

    void createBubble()
    {
        if(Input.GetKeyDown(KeyCode.B)){
        //if(seg){
            burbuja = GameObject.FindGameObjectWithTag("Bubble");
            Debug.Log("S'ha apretat la b");
           if(burbuja!= null){
               if(Vector3.Distance(tr.position, burbuja.GetComponent<Transform>().position) < 16){
                   Debug.Log("Burbuja Cerca");
                   if(burbuja.GetComponent<BurbujaStats>().seguir == true){
                        burbuja.GetComponent<BurbujaStats>().seguir = false;
                        Debug.Log("Seguia");
                    }else{
                        Debug.Log("No Seguia");
                        burbuja.GetComponent<BurbujaStats>().seguir = true;
                    }
               }else{
                    Debug.Log("Ya has creado una burbuja");
               }
           }else{
            Debug.Log("Bubble instantiated");
            Instantiate(bubble, getBubblePosition(), Quaternion.identity);
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
