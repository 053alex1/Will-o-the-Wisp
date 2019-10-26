using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samplescript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    private Transform tr;

    public float speed = 5f;

    public Vector3 move;

    public float jumpHeight = 2f;

    void Start()
    {
        print("Let\'s get this bread.");
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move() {
        //print(Input.GetAxis("Horizontal"));
        move = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed , 0, Input.GetAxis("Vertical")* Time.deltaTime * speed);
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