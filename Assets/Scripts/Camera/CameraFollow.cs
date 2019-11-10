using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Camera maincam;
    private const float min_ang_Y = 0.0f;
    private const float max_ang_Y = 50.0f;
    public float distance = 20.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float senseX = 2.0f; //Sensibilidad del eje X y del Y
    private float senseY = 1.0f;
    private Vector3 offset = new Vector3(0, 10, 0);

    void Start() {
        maincam = Camera.main;
        target = GameObject.FindGameObjectWithTag("Dagda").transform;
    }
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * senseX;
        currentY -= Input.GetAxis("Mouse Y") * senseY;

        currentY = Mathf.Clamp(currentY, min_ang_Y, max_ang_Y);
    }
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        maincam.transform.position = target.position + rotation * dir;
        maincam.transform.LookAt(target.position + offset);
    }
}
