using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float hp = 10f;

    public void getHit(float damage) {
        hp -= damage;
        Debug.Log("ouch - " + hp + " hp left");
        if (hp <= 0) Destroy(gameObject);
    }
}
