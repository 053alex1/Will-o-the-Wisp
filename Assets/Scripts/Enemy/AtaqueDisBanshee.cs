using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueDisBanshee : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxLength = 20f;



    void Start()
    {
        transform.position += 2*transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.position += new Vector3(0f, 0f, Time.deltaTime);
        transform.localScale += new Vector3(0f, Time.deltaTime, 0f);
    }


}
