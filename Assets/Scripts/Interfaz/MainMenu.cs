using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame1() {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   public void MenuGame() {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   } 

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
