using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public ParticleSystem deathparticles;
    public float hp;
    public float mana;
    public float speed = 20f;
    public float jumpHeight = 0.5f;
    public float lightDamage = 2f;
    public float heavyDamage = 5f;
    public bool isGrounded;
    public bool isRunning;
    private const float maxHealth = 100f;
    private const float maxMana = 15f;
    private float manaRegenPerSec = 0.75f;
    private float delay = 1f;   //Tiempo entre el último ataque de maná y la regeneración de este
    private float timestamp = 0f;
    public bool isDead;
    public bool Ultim = false;
    private Animator myAnim;
    public GUIInteraction gui;
    public bool isDying = false;  // para saber si estoy en los 3 segundos de espera




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
            mana = Mathf.Min(maxMana, mana + (manaRegenPerSec * Time.deltaTime));
            gui.ChangeMagic(mana);
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
        //fillHp();
    }
    public void getHit(float damage)
    {
        if (isDead || isDying) return;
        myAnim.SetTrigger("isHurt");
        hp -= damage;
        if (hp > 0) { gui.ChangeLife(hp); Debug.Log("Player ouch - " + hp + " hp left"); }
        else if (hp <= 0)
        {
            StartCoroutine(deadhit());

        }


    }


    public void dead()
    {
        isDead = true;
    }

  //codigo que funciona pero que quita las animaciones
    private IEnumerator deadhit()
    {
        isDying = true;
        GameObject m = GameObject.Find("DagdaMesh");
        m.SetActive(false);
        m = GameObject.Find("DagdaArmature");
        m.SetActive(false);

        Instantiate(deathparticles, transform);

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        isDead = true;
    }

    //codigo que no funciona:
   /* private IEnumerator deadhit()
    {
       
        Instantiate(deathparticles, transform);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        isDead = true;
    }*/

}
