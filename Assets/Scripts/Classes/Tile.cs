using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
  public List<Tile> neighbours;
  public int type; // This value determines the texture of the tile. Eventually implement a 0-X = unwalkable, X-Y = half speed, Y-Z = normal speed
  public int x;
  public int y;

  public Tile()
  {
    neighbours = new List<Tile>();
  }

  public Tile(int x, int y)
  {
    neighbours = new List<Tile>();
    SetCoords(x, y);
  }

  public void SetType(int type)
  {
    this.type = type;
  }

  public void SetCoords(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  // TODO: A distance bettween tiles function?
}
