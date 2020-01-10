using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Clase que gestiona la imagen de inicio
public class SplashMenuManager : MonoBehaviour
{

    const float ImageWidth = 5000.0f,
                TimeOut = 15.0f;

    public enum SplashStates
    {
        Moving,   //Mientras se hace el xoom
        Finish
    }  //Cuando ha pasado el TimeOut

    public SplashStates State;

    float startTime;

    Image image;

    // Use this for initialization
    void Start()
    {
        State = SplashStates.Moving;
        startTime = Time.time;
        image = GetComponent<Image>();
        transform.localScale = new Vector2(2.5F, 2.5F);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case SplashStates.Moving: 

                //La imagen se va haciendo más grande lentamente (se hace un zoom)
                transform.localScale -= new Vector3(0.001F, 0.001F, 0);

                //Si llega a cierto tamaño, para de aumentar
                if (transform.localScale.x <= 1)
                {
                    transform.localScale = new Vector2(1F, 1F);
                }

                break;
            
            case SplashStates.Finish:
                //Una vez se haya acabado el zoom, se pasará automáticamente al Menú inical
                SceneManager.LoadScene(1);
                break;
            default:
                break;
        }

        //Control del paso del tiempo
        if (Time.time - startTime > TimeOut)
        {
            State = SplashStates.Finish;
        }

      
        //También se puede pasar directamente de escena si el jugador se quiere saltar la entradilla
        if (Input.GetKeyDown("s"))
        {
            SceneManager.LoadScene(1);
        }

    }
}