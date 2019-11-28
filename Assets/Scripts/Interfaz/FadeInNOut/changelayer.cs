using UnityEngine;
using UnityEngine.SceneManagement;

public class changelayer : MonoBehaviour
{

    public Animator animator;

    //Se llamará desde el script en el cual se interactúe con Cernunnos
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
