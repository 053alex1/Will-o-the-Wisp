using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Transform tr;
    GameObject player;
    public float speed = 50f;
    public Vector3 move = new Vector3(0, 55, -55);
    public float jumpHeight = 2f;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Dagda");                                // Renombrar el gameobject a Dagda
    }

    void Start()
    {
        print("Let\'s get this bread.");
        tr = player.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move() {
        //print(Input.GetAxis("Horizontal"));
        move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, rb.velocity.y, Input.GetAxis("Vertical")* Time.deltaTime * speed);
        tr.Translate(move);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump")) {
            rb.velocity += -1f * Physics.gravity.normalized * this.CalculateJumpVerticalSpeed(this.jumpHeight);
        }

    }

    float CalculateJumpVerticalSpeed(float height) {
        return Mathf.Sqrt(2f * height * Physics.gravity.magnitude);
    }
}