using UnityEngine;
using System.Collections;

public class EndLevelDoor : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerScript>()==null)
			return;
		GameManager.Instance.EndGame();
	}

}
