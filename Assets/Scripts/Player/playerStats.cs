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
    private float manaTimestamp = 0f;
    public bool isDead;
    public bool Ultim = false;
    private Animator myAnim;
    public GUIInteraction gui;
    public bool isDying = false;  // para saber si estoy en los 3 segundos de espera
    public PlayerController playerCont;
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
        manaTimestamp = Time.time;
        mana = Mathf.Max(0, mana - amount);
    }
    public void regenMana()
    {
        if (Time.time > (manaTimestamp + delay))
        {      // Sólo se recupera el maná cuando hayan pasado los segundos del delay
            mana = Mathf.Min(maxMana, mana + (manaRegenPerSec * Time.deltaTime));
            gui.ChangeMagic(mana);
        }
    }
    void Awake()
    {
        hp = maxHealth;
        mana = maxMana;
        myAnim = GetComponentInChildren<Animator>();
        playerCont = GetComponentInChildren<PlayerController>();
        gui = GameObject.Find("GUI").GetComponent<GUIInteraction>();
    }
    void Update()
    {
        regenMana();
    }
    void Start()
    {
        InvokeRepeating("regenMana", 0f, manaRegenPerSec);  // Con esta función se invoca a regenMana() cada manaRegenPerSec segundos
    }
    public void getHit(float damage)
    {
        if (playerCont.getDead() || isDying) return;
        myAnim.SetTrigger("isHurt");
        hp -= damage;
        if (hp > 0) { gui.ChangeLife(hp); Debug.Log("Player ouch - " + hp + " hp left"); }
        else if (hp <= 0)
        {
            dead();
            StartCoroutine(deadHit());
        }
    }

    public void dead()
    {
        playerCont.setDead(true);
    }

    private IEnumerator deadHit()
    {
        myAnim.SetTrigger("death");
        Instantiate(deathparticles, transform);
        yield return new WaitForSeconds(7);
        gameObject.SetActive(false);
        playerCont.setReallyDead(true);
    }
}
