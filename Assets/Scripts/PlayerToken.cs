using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToken : MonoBehaviour
{
  private State state;
  private Vector3 targetPosition;
  private Action OnMoveComplete;

  private enum State {Idle, Moving}
  // Start is called before the first frame update
  void Start()
  {
    state = State.Idle;
  }

  void Update()
  {
    switch(state)
    {
      case State.Idle:
        break;
      case State.Moving:
        float moveSpeed = 5f;
        transform.position += (targetPosition - GetPosition()) * moveSpeed * Time.deltaTime;
        if(Vector3.Distance(GetPosition(), targetPosition) <= .2f)
        {
          transform.position = targetPosition;
          state = State.Idle;
          OnMoveComplete();
        }
        break;
    }
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public void MoveToPosition(Vector3 targetPosition, Action OnMoveComplete)
  {
    this.targetPosition = targetPosition;
    this.OnMoveComplete = OnMoveComplete;
    state = State.Moving;
  }
}
