using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float hp = 0f;
    public float mana = 20f;
    public float speed = 50f;
    public float jumpHeight = 0.5f;
    public float lightDamage = 2f;
    public float heavyDamage = 5f;
    public bool isGrounded;
    public bool isRunning;
    private float maxHealth = 100f;
    private float manaRegenPerSec = 0.5f;
    private float delay = 3f;   //Tiempo entre el último ataque de maná y la regeneración de este
    private float maxMana = 20f;
    private float timestamp = 0f;
    
    public float reduceHp(float amount) {
        return hp -= amount;
    }

    public void fillHp() {
        hp = maxHealth;
    }

    public void reduceMana(float amount) {
        timestamp = Time.time;              // Cuando se 
        mana = Mathf.Max(0, mana - amount);
    }

    public void regenMana() {
        if (Time.time > (timestamp + delay)) {      // Sólo se recupera el maná cuando hayan pasado los segundos del delay
            mana = Mathf.Min(maxMana, mana + (manaRegenPerSec * Time.deltaTime)); // (* Time.deltaTime) ?
            //Debug.Log("Mana is " + mana);
        }
    }

    void Update() {
        regenMana();
    }
    void Start() {
        InvokeRepeating("regenMana", 0f, manaRegenPerSec);  // Con esta función se invoca a regenMana() cada 0.5 segundos
        fillHp();
    }
    public void getHit(float damage) {
        hp -= damage;
        Debug.Log("ouch - " + hp + " hp left");
        if (hp <= 0) Destroy(gameObject);
    }

}
