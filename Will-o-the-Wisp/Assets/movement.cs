using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class movement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 speed = new Vector2(10, 10);
    public float speed2 = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * Time.deltaTime, 0, moveVertical * Time.deltaTime);
        transform.Translate(moveHorizontal * Time.deltaTime * speed2 ,0.0f, moveVertical * Time.deltaTime * speed2); ;

        rb.AddForce(movement * speed);
    }
}