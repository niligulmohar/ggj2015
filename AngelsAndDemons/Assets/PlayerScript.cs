using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private CharacterController myController;
	public Transform myCamera;

	public float turnSpeed;
	public float moveSpeed;

	private Animator myAnimator;

	public GameObject Damaged;

	// Use this for initialization
	void Start () {
		myController = GetComponent<CharacterController>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(transform.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);

		//myController.SimpleMove(transform.forward * moveSpeed * Input.GetAxis("Vertical"));
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		myController.SimpleMove(moveSpeed * move );


		if (move.sqrMagnitude > 0.1)
			myAnimator.SetBool("walking",true);
		else
			myAnimator.SetBool("walking",false);

		Damaged.SetActive(GameManager.Instance.PlayerLife <=1);
	}
}
