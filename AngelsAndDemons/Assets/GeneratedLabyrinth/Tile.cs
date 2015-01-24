using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  public ArrayList GetPickupSpawnPoints() {
    var result = new ArrayList();
    foreach (Transform child in transform) {
      if (child.name == "Pickup") {
        result.Add(child);
      }
    }
    return result;
  }

  public ArrayList GetTrapSpawnPoints() {
    var result = new ArrayList();
    foreach (Transform child in transform) {
      if (child.name == "Trap") {
        result.Add(child);
      }
    }
    return result;
  }

  public ArrayList GetDecorationSpawnPoints() {
    var result = new ArrayList();
    foreach (Transform child in transform) {
      if (child.name == "Decoration") {
        result.Add(child);
      }
    }
    return result;
  }
}