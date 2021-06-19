using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMap: MonoBehaviour {
  //int[,] tiles;
  Tile[,] tiles;
  int size_x;
  int size_y;

  public void BuildMap(int size_x, int size_y) {
    // Constructor needs to be public if we want other objects to be able to instantiate this
    this.size_x = size_x;
    this.size_y = size_y;
    tiles = new Tile[size_x,size_y];

    // Create tiles
    for(int x=0; x < size_x; x++) {
			for(int y=0; y < size_y; y++) {
        tiles[x,y] = new Tile(x, y);
        int random = Random.Range(0, 100);
        if(random < 60) {
          tiles[x,y].SetType(2);
        }
        else if(random < 75) {
          tiles[x,y].SetType(0);
        }
        else if(random < 90) {
          tiles[x,y].SetType(3);
        }
        else {
          tiles[x,y].SetType(1);
        }
      }
    }

    // Calculate neighbours
    for(int x=0; x < size_x; x++) {
      for(int y=0; y < size_y; y++) {
        if(x >= 1) {
          tiles[x,y].neighbours.Add(tiles[x-1,y]); // left
          if(y >= 1) {
            tiles[x,y].neighbours.Add(tiles[x-1,y-1]); // bottom left
          }
          if(y < size_y-1) {
            tiles[x,y].neighbours.Add(tiles[x-1,y+1]); // top left
          }
        }
        if(x < size_x-1) {
          tiles[x,y].neighbours.Add(tiles[x+1,y]); // right
          if(y >= 1) {
            tiles[x,y].neighbours.Add(tiles[x+1,y-1]); // bottom right
          }
          if(y < size_y-1) {
            tiles[x,y].neighbours.Add(tiles[x+1,y+1]); // top right
          }
        }
        if(y >= 1) {
          tiles[x,y].neighbours.Add(tiles[x,y-1]); // bottom
        }
        if(y < size_y-1) {
          tiles[x,y].neighbours.Add(tiles[x,y+1]); // top
        }
      }
    }
  }

  public int GetTile(int x, int y) {
    return tiles[x,y].type;
  }

  public void SetTile(int x, int y, int type) {
    tiles[x,y].type = type;
  }

  public void ShortestPathTo(int x, int y) {
    // Check if the player can enter that tile
  }
}
