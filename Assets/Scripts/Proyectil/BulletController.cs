using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject effect;
    private float bulletDuration = 5.0f;
    private float bulletForce = 8.0f;
    private float bulletRadius = 2.0f;
    private GameObject player;
    private playerStats ps;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
    }

    void Start()
    {
        Destroy(gameObject.GetComponent<ParticleSystem>(), bulletDuration);
        Destroy(gameObject, bulletDuration);
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
    }
    void OnCollisionEnter(Collision col)
    {
        Rigidbody rbtarget = col.gameObject.GetComponent<Rigidbody>();
        if (rbtarget != null)
        {
            rbtarget.AddExplosionForce(bulletForce, transform.position, bulletRadius);
            if (col.collider.tag == "Enemy")
            {
                col.collider.GetComponent<BaseEnemy>().getHit(ps.lightDamage);
            }
        }
        ContactPoint contact = col.GetContact(0);
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        effect = (GameObject)Instantiate(effect, pos, rot);

        Destroy(gameObject);
        Destroy(gameObject.GetComponent<GameObject>(), 5);
        Destroy(effect);
        Destroy(effect.GetComponent<GameObject>(), 5);
    }


}
