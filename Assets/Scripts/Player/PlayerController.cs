using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    public Animator playerAnimator;
    public GameObject GFX;
    public bool seg = false; //crec que no afecta en res
    public GameObject canvas;
    public GameObject npc;
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
    private Vector3 movegfx;
    private BurbujaStats bs;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
        playerAnimator = GFX.transform.GetComponent<Animator>();
        maincam = Camera.main.transform;
        disparador = GameObject.Find("Disparador");
        shootScript = disparador.GetComponent<Shoot>();
        burbuja = GameObject.FindGameObjectWithTag("Bubble");
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ps.isRunning = true;
            ps.speed *= 2;
            playerAnimator.SetBool("isRunning", true);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ps.isRunning = false;
            ps.speed /= 2;
            playerAnimator.SetBool("isRunning", false);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * 250.0f * Time.deltaTime, 0);
        movegfx = tr.forward * move.magnitude;
        movegfx.y = rb.velocity.y;

        transform.Translate(movegfx * ps.speed * Time.deltaTime, Space.World);

        disparador.transform.rotation = Quaternion.Euler(0f, maincam.eulerAngles.y, 0f);
        playerAnimator.SetFloat("Walking", Mathf.Abs(movegfx.x + movegfx.z));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && ps.isGrounded)
        {
            rb.velocity += -1f * Physics.gravity.normalized * this.CalculateJumpVerticalSpeed(this.ps.jumpHeight);
            ps.isGrounded = false;
            playerAnimator.SetBool("isJumping", true);
        }
    }

    void LightAttack()
    {
        shootScript.lightShoot();
    }

    void HeavyAttack()
    {
        shootScript.heavyShoot();
    }

    void Attack()
    {
        burbuja = GameObject.FindGameObjectWithTag("Bubble");
        //Debug.Log("!bs.seguir is: " + bs.seguir);
        if (Input.GetMouseButtonDown(0))
        {
            if (burbuja != null)
            {
                Debug.Log("Burbuja no es null");
                if (!bs.seguir)
                {
                    Debug.Log("Burbuja no es null - la burbuja no me está siguiendo");
                    if (ps.mana >= 2)
                    {
                        if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dagda_correr")) playerAnimator.SetTrigger("isAttacking");
                        LightAttack();

                    }
                    else { Debug.Log("Not enough mana for light attack - Mana is " + ps.mana); }
                } else { Debug.Log("Burbuja no es null - la burbuja me está siguiendo"); }

            }
            else if (burbuja == null)
            {
                Debug.Log("Burbuja es null pero puedo atacar igualmente");
                if (ps.mana >= 2)
                {
                    if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dagda_correr")) playerAnimator.SetTrigger("isAttacking");
                    LightAttack();

                }
                else { Debug.Log("Not enough mana for light attack - Mana is " + ps.mana); }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (burbuja != null)
            {
                Debug.Log("Burbuja no es null");
                if (!bs.seguir)
                {
                    Debug.Log("Burbuja no es null - la burbuja no me está siguiendo");
                    if (ps.mana >= 5)
                    {
                        if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dagda_correr")) playerAnimator.SetTrigger("isAttacking");
                        HeavyAttack();
                    }
                    else Debug.Log("Not enough mana for heavy attack - Mana is " + ps.mana);
                } else { Debug.Log("Burbuja no es null - la burbuja me está siguiendo"); }
            }
            else if (burbuja == null)
            {
                Debug.Log("Burbuja es null pero puedo atacar igualmente");
                if (ps.mana >= 5)
                {
                    if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dagda_correr")) playerAnimator.SetTrigger("isAttacking");
                    HeavyAttack();
                }
                else Debug.Log("Not enough mana for heavy attack - Mana is " + ps.mana);
            }
        }
    }

    float CalculateJumpVerticalSpeed(float height)
    {
        return Mathf.Sqrt(2f * height * Physics.gravity.magnitude);
    }

    void bubbleFunction()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            burbuja = GameObject.FindGameObjectWithTag("Bubble");
            if (burbuja != null)
            {
                bs = burbuja.GetComponent<BurbujaStats>();
                Debug.Log("La burbuja está creada");
                if (Vector3.Distance(tr.position, burbuja.GetComponent<Transform>().position) < 16)
                {
                    Debug.Log("Burbuja Cerca");
                    if (burbuja.GetComponent<BurbujaStats>().seguir == true)
                    {
                        Debug.Log("Dejando de seguir a Dagda");
                        burbuja.GetComponent<BurbujaStats>().seguir = false;
                    }
                    else
                    {
                        Debug.Log("Siguiendo a Dagda");
                        burbuja.GetComponent<BurbujaStats>().seguir = true;
                    }
                }
                else
                {
                    Debug.Log("Ya has creado una burbuja y estás muy lejos de ella para que te siga");
                    //Hacer algo para que el jugador se dé cuenta
                }
            }
            else
            {
                var b = Instantiate(bubblePrefab);
                //b.transform.position = getBubblePosition();
                b.transform.rotation = Quaternion.identity;
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
        return getPlayerPosition() + new Vector3(0, 0, 0); //Falta por determinar la altura de Dagda
    }

   

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            ps.isGrounded = true;
            playerAnimator.SetBool("isJumping", false);
        }

        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("Has sido dañado");
            playerAnimator.SetBool("isHurting", true);
            ps.reduceHp(20.0f);
            playerAnimator.SetBool("isHurting", false);
            Debug.Log("Ya no eres dañado");

        }
    }

}