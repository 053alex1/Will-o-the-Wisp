using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public Animator playerAnimator;
    public GameObject GFX;

    GameObject player;
    public Vector3 move = new Vector3(0, 55, -55);
    public GameObject bubble;
    public playerStats ps;

    public float RotationSpeed = 100;
    

    //Todos estos valores se moverán a otro script 
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        playerAnimator = GFX.transform.GetComponent<Animator>();
    }

    void Start()
    {
        print("Let\'s get this bread.");
        tr = player.transform.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
        ps.isGrounded = true;
        ps.isRunning = false;
    }

    void FixedUpdate()
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
        
        move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * ps.speed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * ps.speed);
        tr.Translate(move);
        playerAnimator.SetFloat("Walking", Mathf.Abs(move.x + move.z));

        transform.Rotate(0, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime, 0);
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
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Bubble instantiated");
            Instantiate(bubble, getBubblePosition(), Quaternion.identity);
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
