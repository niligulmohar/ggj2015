using UnityEngine;
using System.Collections;

public class EvilMonsterScript : MonoBehaviour {

	private Animator myAnimator;
	private CharacterController myController;

	public Vector3 WalkDirection;

	public float WalkSpeed;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
		myController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 distance = -transform.position;
		
		//myController.SimpleMove(transform.forward * moveSpeed * Input.GetAxis("Vertical"));
		Vector3 move = WalkDirection.normalized * Mathf.Min(WalkDirection.magnitude, 1f);
		myController.SimpleMove(WalkSpeed * move );
		
		distance += transform.position;
		
		bool wallHit = (distance.magnitude  < (move.magnitude * WalkSpeed - 0.1f) * Time.deltaTime);

		if (wallHit) {
			WalkDirection = -WalkDirection;
		}
	
	}
}
