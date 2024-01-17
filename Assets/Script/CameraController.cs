using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField] Transform target;
	[SerializeField] float smoothing = 5f;
	private Vector3 offset = new Vector3(0f,0f,-10f);

	void Start()
	{
		offset = transform.position - target.position;
	}

	void LateUpdate()
	{
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
