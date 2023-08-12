using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerMovement : NetworkBehaviour
{
  // private float moveSpeed = 5f;
  private PlayerInput playerInput;

  private Vector2 moveVector;

  private NetworkVariable<ulong> clientId = new NetworkVariable<ulong>(10, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private PlayerStatus playerStatus;

  private void Awake()
  {
    playerInput = GetComponent<PlayerInput>();
    playerStatus = FindObjectOfType<PlayerStatus>();
  }

  private void Start()
  {
    if (IsOwner)
    {
      clientId.Value = OwnerClientId;
    }
  }

  private void Update()
  {
    if (!IsOwner)
    {
      return;
    }
    if (playerStatus.canMove)
    {
      HandleMovement();
    }

    if (playerInput.GetTypeMove() == 1 && PointManager.Instance.playerPoint[clientId.Value].playerIndex != 0)
    {
      playerStatus.moveSpeed = 7f;
    }
    else if (playerInput.GetTypeMove() == 0)
    {
      playerStatus.moveSpeed = 5f;
    }

    if (playerInput.GetTypeMove() == -1 && PointManager.Instance.playerPoint[clientId.Value].playerIndex != 0)
    {
      playerStatus.moveSpeed = 3f;
    }
  }

  public void SetCanMove(bool canMove)
  {
    if (IsOwner)
      playerStatus.canMove = canMove;
  }
  public Vector2 MoveVector()
  {
    return moveVector;
  }

  private void HandleMovement()
  {
    Vector2 inputVector = playerInput.GetMovementVectorNormalized();
    float moveDistance = playerStatus.moveSpeed * Time.deltaTime;

    transform.position += new Vector3(inputVector.x, inputVector.y) * moveDistance;

    moveVector = inputVector;
  }
  public int GetTypeMove()
  {
    return playerInput.GetTypeMove();
  }

  // public void SetSpeed(float speed)
  // {
  //   moveSpeed = speed;
  // }

  public ulong GetClientId()
  {
    return clientId.Value;
  }
}
