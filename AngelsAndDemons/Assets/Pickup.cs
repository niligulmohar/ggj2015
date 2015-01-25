using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
  public Transform pickupEffect;

  private float initialDelta;
  private float speed = 100;

	public int HumanScore;
	public int DevilScore;
	public int AngelScore;

	public bool rotate;

  void Awake () {
    initialDelta = Random.value * 360;
  }

	public void GiveScore() {
		GameManager.Instance.AngelScore+=AngelScore;
		GameManager.Instance.DevilScore+=DevilScore;
		GameManager.Instance.HumanScore+=HumanScore;
	}

  void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerScript>()==null)
			return;
		GiveScore();
    Destroy(gameObject);
		if (pickupEffect!=null)
	    Instantiate(pickupEffect, transform.position, Quaternion.identity);
  }

  void Update () {
		if (rotate)
 	   transform.eulerAngles = new Vector3(90, initialDelta + Time.time * speed, 0);
  }
}