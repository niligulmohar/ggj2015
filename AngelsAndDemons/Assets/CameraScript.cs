using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform myTarget;
	public float lerpTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, myTarget.position, lerpTime);
	}

	public void SetLife(float playerLife) {

	}
}
