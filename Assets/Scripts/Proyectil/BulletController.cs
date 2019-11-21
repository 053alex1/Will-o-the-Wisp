using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject bullet;
    private float bulletDuration = 5.0f;
    private float bulletForce = 10.0f;
    private float bulletRadius = 2.0f;
    private GameObject player;

    void Awake () {
        player = GameObject.FindGameObjectWithTag("Dagda");
    }

    void Start() {
        Destroy(gameObject, bulletDuration);
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
    }
    void OnCollisionEnter(Collision col) {
        Debug.Log("Collision encountered");
        if (col.gameObject.name == "Dagda") {
            
            Debug.Log("Ignored collision with Dagda");
        }

        Debug.Log("Collision name: " + col.gameObject.name);

        Rigidbody rbtarget = col.gameObject.GetComponent<Rigidbody>();
        if (rbtarget != null) {
            rbtarget.AddExplosionForce(bulletForce, transform.position, bulletRadius);
        }

        Destroy(gameObject);
    }
}
