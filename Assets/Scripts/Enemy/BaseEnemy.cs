using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float hp = 10f;
    private Transform target;
    GameObject canvas;

    GameObject x;
    // Use this for initialization
    void Start() {
       target = Camera.main.transform;
       canvas = transform.Find("Canvas").gameObject;
    }
   
    // Update is called once per frame
    void Update() {
        Vector3 v = target.position - transform.position;
        v.Normalize();
        canvas.transform.rotation = Quaternion.LookRotation(new Vector3(v.x,0,v.z));
    }
   


    public void getHit(float damage) {
        hp -= damage;
        Debug.Log("ouch - " + hp + " hp left");
        if (hp <= 0) Destroy(gameObject);
    }
}
