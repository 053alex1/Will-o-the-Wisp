using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgTutorial : MonoBehaviour
{
    public GameObject dagda;
    public playerStats ps;
    public int index = 0;

    public void Awake()
    {
        dagda = GameObject.FindGameObjectWithTag("Dagda");
        ps = dagda.GetComponent<playerStats>();
    }
    public string[] msg = {
        "Bienvenido al modo tuturial, para interactuar con el entorno pulsa i",
        "El hechizo que utilizasteis para encerrar a los Dioses Fomoré se está rompiendo. Su regreso está cerca. Como Dios guardián, tu misión es evitar que escapen.",
        "Criaturas malignas están ya escapando. Pero los restos de los Dioses Tuatha De Dannan también; reúnelos a todos y acabad por fin con la amenaza Fomoré.",
        "De los Tuatha De Dannan solo quedan restos de magia en forma de Fuegos Fatuos. Cada Dios se ha dividido en 5 Fuegos. Encuéntralos a todos y llévalos hasta su altar para poder revivir a cada uno de tus compañeros.",
        "Transporta a los Fuegos con una burbuja que crearás gracias a tu magia. Pulsa “B” para crearla y recoge el Fuego acercándote.",
        "Ten cuidado con las criaturas que ya han escapado, pues intentarán evitar que lleves a cabo tu misión.",
        "Puedes atacarlos lanzándoles hechizos con tu magia: botón izquierdo del ratón es un ataque rápido, y el botón derecho es un ataque fuerte.",
        "¡Pero cuidado! No podrás atacar mientras estés sosteniendo la burbuja. Vuelve a pulsar “B” para dejarla y ataca, pero no dejes que la ataquen a ella.",
        "Bien hecho. No todos los enemigos serán tan dóciles como este. Si te es necesario, también puedes correr pulsando “shift” o saltar con la tecla de “espacio.",
        "Dagda, estás listo para emprender la misión. Explora el bosque de Névet y recupera a los Dioses; devuelve la magia a este mundo."
    };
    public string GetMsg() {
        return msg[index];
    }
    public void okButon(){
        if (index == msg.Length - 1) {
            ps.Ultim = true;
        }else{
            index++;
        }
    }
    public void resetIndex() {
        index = 0;
    }
}
