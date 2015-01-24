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

	public bool FootstepLeft;

	public AudioSource WallHitSound;

	public float FootstepFrequenzy;

	private Vector3 LastFootstep;

	public bool moving;

	// Use this for initialization
	void Start () {
		myController = GetComponent<CharacterController>();
		myAnimator = GetComponent<Animator>();



	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.myGameState!= GameManager.GameState.GameRunning)
			return;
		//transform.Rotate(transform.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);

		Vector3 distance = -transform.position;

		//myController.SimpleMove(transform.forward * moveSpeed * Input.GetAxis("Vertical"));
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		move = move.normalized * Mathf.Min(move.magnitude, 1f);
		myController.SimpleMove(moveSpeed * move );

		distance += transform.position;
		
		bool wallHit = (distance.magnitude  < (move.magnitude * moveSpeed - 0.1f) * Time.deltaTime);

		if (wallHit && !WallHitSound.isPlaying) {
			WallHitSound.Play();
			WallHitSound.transform.localPosition = move;
		}

		// Footstep
		if ((LastFootstep-transform.position).magnitude > FootstepFrequenzy) {
			Footstep.pitch = 0.3f + Random.Range(-0.1f,0.3f) + move.magnitude;

			//if (FootstepLeft)
				Footstep.transform.localPosition = - Footstep.transform.localPosition;

			Footstep.Play();
			LastFootstep = transform.position;
		}

		if (move.sqrMagnitude > 0.1) {
			myAnimator.SetBool("walking",true);
		} else {
			myAnimator.SetBool("walking",false);
		}
		Bleeding = GameManager.Instance.PlayerLife <=1;

		Damaged.enableEmission = Bleeding;
		/*if (!DamagedSound.loop && Bleeding) {

			DamagedSound.Play();
		}*/
		DamagedSound.mute = !Bleeding;
	}
}
