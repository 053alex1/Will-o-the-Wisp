using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector3 position;

	void LateUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
		position = target.position + new Vector3(0f, 0f, 100f);
		transform.LookAt(position);
	}
}
