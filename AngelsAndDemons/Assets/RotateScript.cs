using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() {
		transform.Rotate(new Vector3(0,0,1)*Time.fixedDeltaTime);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
