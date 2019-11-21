using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private playerStats ps;
    private GameObject player;
    public GameObject lightPrefab;
    public GameObject heavyPrefab;
    public float bulletForce = 2500.0f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Dagda");
        ps = player.GetComponent<playerStats>();
    }
    public void lightShoot()
    {
        shoot(lightPrefab);
    }
    public void heavyShoot()
    {
        shoot(heavyPrefab);
    }
    void shoot(GameObject bullet)
    {
        Rigidbody rbullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rbullet.AddForce(transform.forward * bulletForce * Time.deltaTime, ForceMode.Impulse);
    }

}
