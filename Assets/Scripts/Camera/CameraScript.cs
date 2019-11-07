using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public  float RotationSpeed = 5.0f;
    private Transform target;
	public float smoothSpeed;
	public Vector3 offset;
    public Vector3 offset2 = new Vector3(0, 2, 0);
    public bool LookAtPlayer = true;
    public bool RotateAroundPlayer = false;

    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Dagda").transform;
        offset = transform.position - target.position;
        transform.LookAt(target);
    }

    void LateUpdate ()
	{
        RotateCheck();

        if(RotateAroundPlayer)
        {
            Quaternion camTurnAngle =
            Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            offset = camTurnAngle * offset;
        }

        Vector3 newPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothSpeed);

        if(LookAtPlayer || RotateAroundPlayer)
		    transform.LookAt(target);
	}

    void RotateCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            RotateAroundPlayer = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            RotateAroundPlayer = false;
        }
    }
}
