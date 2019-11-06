using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float RotationSpeed = 5.0f;
    public Transform target;

	public float smoothSpeed;
	public Vector3 offset;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = false;

    private void Start()
    {
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
