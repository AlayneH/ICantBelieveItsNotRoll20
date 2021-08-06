using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
  public List<Tile> neighbours;
  public int texture; // This value determines the texture of the tile. Eventually implement a 0-X = unwalkable, X-Y = half speed, Y-Z = normal speed
  public int occupant; // This value holds what occupies this space 0 = nothing, 1 = player, 2 = enemy, 3 = other
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

  public void SetTexture(int texture)
  {
    this.texture = texture;
  }

  public void SetOccupant(int occupant)
  {
    this.occupant = occupant;
  }

  public void SetCoords(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  // TODO: A distance bettween tiles function?
}
