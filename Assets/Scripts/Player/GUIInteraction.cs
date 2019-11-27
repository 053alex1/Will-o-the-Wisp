using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteraction : MonoBehaviour
{
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GameObject.FindGameObjectWithTag("life");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLife(float health)
    {
        Debug.Log("2");
        healthBar.fillAmount = health / 100f;
    }
}
