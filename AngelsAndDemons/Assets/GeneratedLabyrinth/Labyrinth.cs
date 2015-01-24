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
    float xPos = ((float)x - (gridSize - 1f) / 2.0f) * tileSize;
    float yPos = ((float)y - (gridSize - 1f) / 2.0f) * tileSize;
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
    for(int x = 0; x < n / 2; x++){
      for(int y = 0; y < n / 2; y++) {
        switch(tiles[x,y]){
        case 0:
          PlaceTile(x,y,4,0);
          break;
        case 1:
          PlaceTile(x,y,3,0);
          break;
        case 2:
          PlaceTile(x,y,3,1);
          break;
        case 3:
          PlaceTile(x,y,1,1);
          break;
        case 4:
          PlaceTile(x,y,3,2);
          break;
        case 5:
          PlaceTile(x,y,2,0);
          break;
        case 6:
          PlaceTile(x,y,1,2);
          break;
        case 7:
          PlaceTile(x,y,0,2);
          break;
        case 8:
          PlaceTile(x,y,3,3);
          break;
        case 9:
          PlaceTile(x,y,1,0);
          break;
        case 10:
          PlaceTile(x,y,2,1);
          break;
        case 11:
          PlaceTile(x,y,0,1);
          break;
        case 12:
          PlaceTile(x,y,1,3);
          break;
        case 13:
          PlaceTile(x,y,0,0);
          break;
        case 14:
          PlaceTile(x,y,0,3);
          break;
        case 15:
          break;
        }
      }
    }
  }
}
