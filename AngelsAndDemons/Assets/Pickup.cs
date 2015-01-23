using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
  public Transform pickupEffect;

  private float initialDelta;
  private float speed = 100;

  void Awake () {
    initialDelta = Random.value * 360;
  }

  void OnTriggerEnter(Collider other) {
    Destroy(gameObject);
    Instantiate(pickupEffect, transform.position, Quaternion.identity);
  }

  void Update () {
    transform.eulerAngles = new Vector3(90, initialDelta + Time.time * speed, 0);
  }
}