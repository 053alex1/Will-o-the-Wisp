using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaStats : MonoBehaviour
{
    public float amplitud = 0.5f;
    public float frecuencia = 1f;
    public float resistencia;
    public Transform[] fuegoFatuos;
    public bool seguir = false;
    public float speed = 50f;
    public bool exist;
    public bool quet = true;
    public const float maxRes = 15f;
    public float radio;
    public GUIInteraction gui;

    void Awake() {
        gui = GameObject.Find("GUI").GetComponent<GUIInteraction>();
        resistencia = maxRes;
        exist = true;
        seguir = true;
        //radio = GetComponent<SphereCollider>().radius;
        radio = 2f;
    }
    public void setExist(bool exist) {
        this.exist = exist;
    }
    public bool getExist() {
        return exist;
    }
    public float getRes() {
        return resistencia;
    }
    public void setSeguir(bool seguir) {
        this.seguir = seguir; 
    }
    public bool getSeguir() {
        return seguir; 
    }
    
    public void dañoRecibido() {
        resistencia =- 1;
        gui.ChangeEnergy(resistencia);
    }
}