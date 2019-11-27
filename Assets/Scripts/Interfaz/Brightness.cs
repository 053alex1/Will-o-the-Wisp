using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness : MonoBehaviour
{

    //float rbgValue = 0.5f;
    // Start is called before the first frame update
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    public void Setbrillo (float brillo) {
        RenderSettings.ambientIntensity = brillo;
        Debug.Log(brillo);
    }
}
