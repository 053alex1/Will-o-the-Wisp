using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness : MonoBehaviour
{

    float rbgValue = 0.5f;
    // Start is called before the first frame update
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        //rbgValue = GUI.HorizontalSlider (new Rect (Screen.vidth/2 -50, 90, 200, 30), rbgValue, 0f, 1.0f);
        RenderSettings.ambientLight = new Color (rbgValue,rbgValue,rbgValue,1);
    }
}
