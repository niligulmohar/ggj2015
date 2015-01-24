using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {
  public Transform effect;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerScript>()==null)
			return;
		GameManager.Instance.DamagePlayer();
		Instantiate(effect, transform.position, Quaternion.identity);
	}

}
