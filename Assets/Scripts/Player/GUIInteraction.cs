using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInteraction : MonoBehaviour
{
    private int counterSpirit = 0;
    
    public Image healthBar;
    public Image magicBar;
    public Image energyBar;

    public GameObject bubbleBar;

    public Text spirits;

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
        healthBar.fillAmount = health / 100f;
    }

    public void ChangeEnergy(float energy)
    {
        energyBar.fillAmount = energy / 15f;
    }

    public void ChangeMagic(float magic)
    {
        magicBar.fillAmount = magic / 20f;
    }

    public void AddSpirit()
    {
        counterSpirit++;
        spirits.text = counterSpirit.ToString();
    }

    public void CreateEnergyBar()
    {
        energyBar.fillAmount = 1;
        bubbleBar.SetActive(true);
    }

    public void DestroyEnergyBar()
    {
        bubbleBar.SetActive(false);

    }
}
