using System;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
  public float tileSize = 8;
  public int gridSize = 7;

  public Tile[] tiles;

  public Transform[] pickupSpawns;
  public Transform[] trapSpawns;
  public Transform[] decorationSpawns;

  Tile PlaceTile (int x, int y, int tileIndex, int rotation) {
    float xPos = ((float)x - gridSize / 2) * tileSize;
    float yPos = ((float)y - gridSize / 2) * tileSize;
    var pos = new Vector3(xPos, 0, yPos);
    var rot = Quaternion.Euler(0, rotation * 90, 0);
    Tile newTile = (Tile)Instantiate(tiles[tileIndex], pos, rot);
    return newTile;
  }

  public void Awake ()
  {
    System.Random rand = new System.Random ();
    int n = gridSize * 2 + 1;

    bool[,] grid = new bool[n, n];

    int oddShape = (n / 2) * 2 - 1;
    int evenShape = (n / 2) * 2;
    int complexity = 5 * evenShape;
    int density = (evenShape * evenShape) / 8;


    // fill outside
    for (int i = 0; i < n; i++) {
      grid [i, 0] = true;
      grid [0, i] = true;
      grid [i, n - 1] = true;
      grid [n - 1, i] = true;
    }

    for (int foo = 0; foo < density; foo++) {
      int x = rand.Next (0, oddShape / 2) * 2;
      int y = rand.Next (0, oddShape / 2) * 2;

      grid [x, y] = true;

      for (int bar = 0; bar < complexity; bar++) {
        int[] x_neighbours = new int[4];
        int[] y_neighbours = new int[4];
        int index = 0;

        if (x > 1) {
          x_neighbours [index] = x - 2;
          y_neighbours [index] = y;
          index++;
        }
        if (x < oddShape - 2) {
          x_neighbours [index] = x + 2;
          y_neighbours [index] = y;
          index++;
        }
        if (y > 1) {
          x_neighbours [index] = x;
          y_neighbours [index] = y - 2;
          index++;
        }
        if (y < oddShape - 2) {
          x_neighbours [index] = x;
          y_neighbours [index] = y + 2;
          index++;
        }

        if (index > 0) {
          index = rand.Next (0, index);
          int x_ = x_neighbours [index];
          int y_ = y_neighbours [index];

          if (grid [x_, y_] == false) {
            grid [x_, y_] = true;
            grid [y_ + (y - y_) / 2, x_ + (x - x_) / 2] = true;
            x = x_;
            y = y_;
          }
        }
      }
    }

    int[,] tileGrid = new int[n / 2, n / 2];

    for (int x_ = 0; x_ < (n / 2); x_++) {
      for (int y_ = 0; y_ < (n / 2); y_++) {
        int neighbours = 0;
        int x = x_ * 2 + 1;
        int y = y_ * 2 + 1;
        if (grid[x,y - 1]) {neighbours += 8; }
        if (grid[x + 1,y]) {neighbours += 4;}
        if (grid[x,y + 1]) {neighbours += 2;}
        if (grid[x - 1,y]) {neighbours += 1;}
        tileGrid[x_,y_] = neighbours;
      }
    }

    // var line = "";
    // for (int x = 0; x < (n / 2); x++) {
    //   for (int y = 0; y < (n / 2); y++) {
    //     line += tileGrid[x,y].ToString() + " ";
    //   }
    //   line += "\n";
    // }
    // UnityEngine.Debug.Log (line);

    PlaceTiles(tileGrid);
  }

  void PlaceTiles (int[,] tiles) {
    var n = gridSize * 2 + 1;
    Tile tile;
    int type;
    int rotation;
    float pickupChance;
    float trapChance;
    float decorationChance;
    for(int x = 0; x < n / 2; x++){
      for(int y = 0; y < n / 2; y++) {
        tile = null;
        type = -1;
        rotation = 0;
        pickupChance = 0.5f;
        trapChance = 0.15f;
        decorationChance = 0.3f;
        switch(tiles[x,y]){
        case 0:
          type = 4;
          rotation = 0;
          break;
        case 1:
          type = 3;
          rotation = 0;
          break;
        case 2:
          type = 3;
          rotation = 1;
          break;
        case 3:
          type = 1;
          rotation = 1;
          break;
        case 4:
          type = 3;
          rotation = 2;
          break;
        case 5:
          type = 2;
          rotation = 0;
          break;
        case 6:
          type = 1;
          rotation = 2;
          break;
        case 7:
          type = 0;
          rotation = 2;
          break;
        case 8:
          type = 3;
          rotation = 3;
          break;
        case 9:
          type = 1;
          rotation = 0;
          break;
        case 10:
          type = 2;
          rotation = 1;
          break;
        case 11:
          type = 0;
          rotation = 1;
          break;
        case 12:
          type = 1;
          rotation = 3;
          break;
        case 13:
          type = 0;
          rotation = 0;
          break;
        case 14:
          type = 0;
          rotation = 3;
          break;
        case 15:
          break;
        }

        if (type == 0) {
          pickupChance = 1;
        }

        if (x == (int)(n / 4f) && y == (int)(n / 4f)) {
          pickupChance = 0;
          trapChance = 0;
        }

        if (type != -1) {
          tile = PlaceTile(x, y, type, rotation);
          foreach (Transform spawn in tile.GetPickupSpawnPoints()) {
            if (UnityEngine.Random.value < pickupChance) {
              int pickupType = (int)(UnityEngine.Random.value * pickupSpawns.Length);
              Instantiate(pickupSpawns[pickupType], spawn.position, Quaternion.identity);
            }
          }
          foreach (Transform spawn in tile.GetTrapSpawnPoints()) {
            if (UnityEngine.Random.value < trapChance) {
              int trapType = (int)(UnityEngine.Random.value * trapSpawns.Length);
              Instantiate(trapSpawns[trapType], spawn.position, Quaternion.identity);
            }
          }
          foreach (Transform spawn in tile.GetDecorationSpawnPoints()) {
            if (UnityEngine.Random.value < decorationChance) {
              int decorationType = (int)(UnityEngine.Random.value * decorationSpawns.Length);
              Instantiate(decorationSpawns[decorationType], spawn.position, Quaternion.identity);
            }
          }
        }
      }
    }
  }
}
