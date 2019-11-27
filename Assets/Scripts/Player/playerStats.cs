using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float hp;
    public float mana;
    public float speed = 20f;
    public float jumpHeight = 0.5f;
    public float lightDamage = 2f;
    public float heavyDamage = 5f;
    public bool isGrounded;
    public bool isRunning;
    private const float maxHealth = 100f;
    private float manaRegenPerSec = 0.5f;
    private float delay = 3f;   //Tiempo entre el último ataque de maná y la regeneración de este
    private const float maxMana = 20f;
    private float timestamp = 0f;
    public bool isDead;
    public bool Ultim = false;
    private Animator myAnim;
    public GUIInteraction gui;


    public float getHp()
    {
        return hp;
    }
    public float getMana()
    {
        return mana;
    }
    public void fillHp()
    {
        hp = maxHealth;
    }
    public void reduceMana(float amount)
    {
        timestamp = Time.time;              // Cuando se 
        mana = Mathf.Max(0, mana - amount);
    }
    public void regenMana()
    {
        if (Time.time > (timestamp + delay))
        {      // Sólo se recupera el maná cuando hayan pasado los segundos del delay
            mana = Mathf.Min(maxMana, mana + (manaRegenPerSec * Time.deltaTime)); // (* Time.deltaTime) ?
            gui.ChangeMagic(mana);
            //Debug.Log("Mana is " + mana);
        }
    }
    void Awake()
    {
        hp = maxHealth;
        mana = maxMana;
        isDead = false;
        myAnim = GetComponentInChildren<Animator>();
        gui = GameObject.Find("GUI").GetComponent<GUIInteraction>();
    }
    void Update()
    {
        regenMana();
    }
    void Start()
    {
        InvokeRepeating("regenMana", 0f, manaRegenPerSec);  // Con esta función se invoca a regenMana() cada manaRegenPerSec segundos
        fillHp();
    }
    public void getHit(float damage)
    {
        myAnim.SetTrigger("isHurt");
        hp -= damage;
        if (hp > 0) Debug.Log("Player ouch - " + hp + " hp left");
        else if (hp <= 0)
        {
            gameObject.SetActive(false);
            isDead = true;
        }
    }

    public void dead()
    {
        isDead = true;
    }

}
