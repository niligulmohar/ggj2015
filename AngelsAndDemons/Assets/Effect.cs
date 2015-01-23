using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
  void Awake () {
    Invoke("Die", 1);
  }

  void Die() {
    Destroy(gameObject);
  }
}