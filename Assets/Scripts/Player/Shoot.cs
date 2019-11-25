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
        Debug.Log("Light Attack triggered - Mana is " + ps.mana);
        shoot(ps.lightDamage, lightPrefab);
    }
    public void heavyShoot()
    {
        Debug.Log("Heavy Attack triggered - Mana is " + ps.mana);
        shoot(ps.heavyDamage, heavyPrefab);
    }
    void shoot(float manaCost, GameObject bullet)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        bool check = Physics.Raycast(ray, out hit, Mathf.Infinity);

        if (check)
        {
            Debug.DrawRay(transform.position, ray.direction * 1000, Color.red, 10f);
            Debug.Log("Attack did hit something");
            ps.reduceMana(manaCost);

        }
        else
        {
            Debug.DrawRay(transform.position, ray.direction * 1000, Color.white, 10f);
            Debug.Log("Attack did not hit");
        }

        Rigidbody rbullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rbullet.AddForce(ray.direction * bulletForce * Time.deltaTime, ForceMode.Impulse);
    }

}
