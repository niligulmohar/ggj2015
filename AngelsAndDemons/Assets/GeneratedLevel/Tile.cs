using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  public Transform[] randomSpawns;
  public void Init(int x, int y) {
    foreach(var transform in randomSpawns) {
      Debug.Log("TODO: Maybe spawn");
    }
  }
}