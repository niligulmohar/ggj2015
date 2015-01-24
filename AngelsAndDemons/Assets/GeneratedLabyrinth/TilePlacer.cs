using UnityEngine;
using System.Collections;

public class TilePlacer : MonoBehaviour {
  public float tileSize = 8;
  public int gridSize = 3;
  public Tile[] availableTiles;
  public Transform[] availableSpawns;

  void Awake () {

    for (int x = 0; x < gridSize; x++) {
      for (int y = 0; y < gridSize; y++) {
        float xPos = ((float)x - (gridSize - 1f) / 2.0f) * tileSize;
        float yPos = ((float)y - (gridSize - 1f) / 2.0f) * tileSize;
        var pos = new Vector3(xPos, 0, yPos);
        var tileIndex = (int)(Random.value * availableTiles.Length);
        Tile newTile = (Tile)Instantiate(availableTiles[tileIndex], pos, Quaternion.identity);

        newTile.Init(x, y);
        foreach (var transform in newTile.randomSpawns) {
          if (Random.value > 0.5) {
            var spawnIndex = (int)(Random.value * availableSpawns.Length);
            Instantiate(availableSpawns[spawnIndex], transform.position, Quaternion.identity);
          }
        }
      }
    }
  }
}
