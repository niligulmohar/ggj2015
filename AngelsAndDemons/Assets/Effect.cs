using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	public float Lifetime=1;
  void Awake () {
    Invoke("Die", Lifetime);
  }

  void Die() {
    Destroy(gameObject);
  }
}