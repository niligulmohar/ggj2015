using UnityEngine;
using System.Collections;

public class RandomizeSoundStart : MonoBehaviour {


	// Use this for initialization
	void Start () {
		var s = GetComponent<AudioSource>();
		if (s==null)
			return;
		s.time = Random.Range(0, s.clip.length);
	}

}
