using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changelayer : MonoBehaviour
{

    public Animator animator; 


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel(2);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
