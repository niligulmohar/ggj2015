using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private CharacterController myController;
	public Transform myCamera;

	public float turnSpeed;
	public float moveSpeed;

	private Animator myAnimator;

	public ParticleSystem Damaged;

	public AudioSource DamagedSound;

	private bool Bleeding;

	public AudioSource Footstep;

	public float FootstepFrequenzy;

	private Vector3 LastFootstep;

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

		// Footstep
		if ((LastFootstep-transform.position).magnitude > FootstepFrequenzy) {
			Footstep.Play();
			LastFootstep = transform.position;
		}

		if (move.sqrMagnitude > 0.1)
			myAnimator.SetBool("walking",true);
		else
			myAnimator.SetBool("walking",false);

		Bleeding = GameManager.Instance.PlayerLife <=1;

		Damaged.enableEmission = Bleeding;
		if (!DamagedSound.loop && Bleeding) {

			DamagedSound.Play();
		}
		DamagedSound.loop = Bleeding;
	}
}
