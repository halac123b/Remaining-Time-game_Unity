using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  private float moveSpeed = 5f;
  private PlayerInput playerInput;

  private Vector2 moveVector;
  private Vector2 lastDirection;

  private bool canMove = true;

  [SerializeField] int processSpeed = 5;

  private void Awake()
  {
    lastDirection = new Vector2(0, 1);
    playerInput = FindObjectOfType<PlayerInput>();
  }

  private void Update()
  {
    if (!IsOwner)
    {
      return;
    }
    if (canMove)
    {
      HandleMovement();
    }

    if (playerInput.GetTypeMove() == 1)
    {
      moveSpeed = 7f;
    }
    else if (playerInput.GetTypeMove() == 0)
    {
      moveSpeed = 5f;
    }

    if (playerInput.GetTypeMove() == -1)
    {
      moveSpeed = 3f;
    }
  }

  public void SetCanMove(bool canMove){
    this.canMove = canMove;
  }
  public Vector2 MoveVector()
  {
    return moveVector;
  }

  public Vector2 LastDirection()
  {
    return lastDirection;
  }

  private void HandleMovement()
  {
    Vector2 inputVector = playerInput.GetMovementVectorNormalized();
    float moveDistance = moveSpeed * Time.deltaTime;


    transform.position += new Vector3(inputVector.x, inputVector.y) * moveDistance;

    lastDirection = moveVector;
    moveVector = inputVector;
  }
  public int GetTypeMove()
  {
    return playerInput.GetTypeMove();
  }

  public void SetSpeed(float speed)
  {
    moveSpeed = speed;
  }

  

  
}

// Fix: Remove LastDirection
