using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMap: MonoBehaviour {
  int[,] tiles;
  int size_x;
  int size_y;

  public void BuildMap(int size_x, int size_y) {
    // Constructor needs to be public if we want other objects to be able to instantiate this
    this.size_x = size_x;
    this.size_y = size_y;

    tiles = new int[size_x,size_y];

    for(int x=0; x < size_x; x++) {
			for(int y=0; y < size_y; y++) {
        int random = Random.Range(0, 100);
        if(random < 60) {
          tiles[x,y] = 2;
        }
        else if(random < 75) {
          tiles[x,y] = 0;
        }
        else if(random < 90) {
          tiles[x,y] = 3;
        }
        else {
          tiles[x,y] = 1;
        }
      }
    }
  }

  public int GetTile(int x, int y) {
    return tiles[x, y];
  }

  public void SetTile(int x, int y, int type) {
    tiles[x,y] = type;
  }
}
