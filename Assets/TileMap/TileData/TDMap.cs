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
        tiles[x,y].SetTexture(2);
        tiles[x,y].SetOccupant(0);
      }
    }

    // Hard coded charcater spawn locations
    tiles[5,5].SetOccupant(1);
    tiles[6,5].SetOccupant(1);
    tiles[7,5].SetOccupant(1);
    tiles[8,5].SetOccupant(1);
    tiles[9,5].SetOccupant(1);
    tiles[9,9].SetOccupant(2);

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
    return tiles[x,y].texture;
  }

  public void SetTile(int x, int y, int texture) {
    tiles[x,y].texture = texture;
  }

  public int GetOccupant(int x, int y) {
    return tiles[x,y].occupant;
  }

  public void SetOccupant(int x, int y, int occupant) {
    tiles[x,y].occupant = occupant;
  }

  public void ShortestPathTo(int x, int y) {
    // Check if the player can enter that tile
  }
}
