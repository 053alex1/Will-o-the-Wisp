using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tr;
    GameObject player;
    public float speed = 50f;
    public Vector3 move = new Vector3(0, 55, -55);
    public float jumpHeight = 2f;
    private bool isGrounded;
    public GameObject bubble;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
    }
    // Start is called before the first frame update
    void Start()
    {
        print("Let\'s get this bread.");
        tr = player.transform.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        createBubble();
        Attack();
    }

    void Move()
    {
        move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        tr.Translate(move);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity += -1f * Physics.gravity.normalized * this.CalculateJumpVerticalSpeed(this.jumpHeight);
            isGrounded = false;
        }
    }

    void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.red, 10f);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, ray.direction * 1000, Color.white, 10f);
                Debug.Log("Did not Hit");
            }
        }
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
        if (collision.collider.tag == "Ground") isGrounded = true;
    }
}
