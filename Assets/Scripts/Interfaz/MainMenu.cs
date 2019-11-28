using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame1() {
       SceneManager.LoadScene(1);
   }
   public void MenuGame() {
       SceneManager.LoadScene(0);
   }
   public void PlaySameGame() {
       Debug.Log("ReintentarNivell");
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
