using UnityEngine;
using System.Collections;

public class EvilMonsterScript : MonoBehaviour {

	private Animator myAnimator;
	private CharacterController myController;

	public Vector3 WalkDirection;

	public float WalkSpeed;

	// Use this for initialization
	void Start () {

		transform.position += new Vector3(0,1,0);

		myAnimator = GetComponent<Animator>();
		myController = GetComponent<CharacterController>();

		if (Random.Range(0.0f,1f)>0.5)
			WalkDirection = new Vector3(0,0,1);
		else
			WalkDirection = new Vector3(1,0,0);

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.myGameState!= GameManager.GameState.GameRunning)
			return;

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
