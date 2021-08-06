// TODO: Hide cursor when mouse hovers over button
// Do not allow the user to move to a tile under the buttons when they are hovered over the button
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TDMap))]
public class TileMapMouse : MonoBehaviour {
  TDMap _tileMap;
  public Vector3 currentTileCoord;
  public Vector3 selectedTileCoord;
  public GameObject selection;
  private State state;
  private Action OnSelectComplete;
  private enum State {Idle, Listening}

  void Start() {
    // C# standard, _[variableName] indicates that the object it a private variable
    _tileMap = GetComponent<TDMap>();
    state = State.Idle;
  }

  void Update() {
    GetPosition();
    selection.transform.position = currentTileCoord;
    if(state == State.Listening) {
      if(Input.GetMouseButtonDown(0)) {
        if(currentTileCoord.x != -1)
        {
          selectedTileCoord = currentTileCoord;
          state = State.Idle;
          Debug.Log("Tile selected");
          OnSelectComplete();
        }
      }
    }
  }

  public void GetPosition() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitInfo;
    Collider collider = GetComponent<Collider>();

    if(collider.Raycast(ray, out hitInfo, Mathf.Infinity)) {
      selection.SetActive(true);
      float factor = 5f;
      float x = Mathf.Round(hitInfo.point.x / factor) * factor;
      float z = Mathf.Round(hitInfo.point.z / factor) * factor;
      currentTileCoord.x = x;
      currentTileCoord.y = .1f;
      currentTileCoord.z = z;
    }
    else{
      selection.SetActive(false);
      currentTileCoord.x = -1f;
      currentTileCoord.y = .1f;
      currentTileCoord.z = -1f;
    }
  }

  public void Select(Action OnSelectComplete)
  {
    this.OnSelectComplete = OnSelectComplete;
    state = State.Listening;
  }
}
