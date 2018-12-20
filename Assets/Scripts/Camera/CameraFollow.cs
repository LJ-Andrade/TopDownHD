using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform followTarget;
	public Vector3 targetOffset;
	public float moveSpeed;
	private Transform camTransform;

	// Use this for initialization
	void Start () {
		camTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(followTarget && followTarget.gameObject.activeSelf)
		{
			camTransform.position = Vector3.Slerp(camTransform.position, followTarget.position + targetOffset, moveSpeed);
		}
	}
}
