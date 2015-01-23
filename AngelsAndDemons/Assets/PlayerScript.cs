using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private CharacterController myController;
	public Transform myCamera;

	public float turnSpeed;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		myController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(transform.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);

		myController.SimpleMove(transform.forward * moveSpeed * Input.GetAxis("Vertical"));
		
	
	}
}
