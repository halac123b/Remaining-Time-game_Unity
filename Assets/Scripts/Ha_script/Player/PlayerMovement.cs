using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  [SerializeField] private float moveSpeed = 7f;
  private GameInput gameInput;

  private Vector2 walkVector;
  private Vector2 lastDirection;

  private void Awake()
  {
    lastDirection = new Vector2(0, 1);
    gameInput = FindObjectOfType<GameInput>();
  }

  private void Update()
  {
    if (!IsOwner)
    {
      return;
    }
    HandleMovement();
  }

  public Vector2 WalkVector()
  {
    return walkVector;
  }

  public Vector2 LastDirection()
  {
    return lastDirection;
  }

  private void HandleMovement()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, inputVector.y);
    float moveDistance = moveSpeed * Time.deltaTime;

    transform.position += moveDir * moveDistance;

    lastDirection = walkVector;
    walkVector = moveDir;
  }
}

// Fix: Remove LastDirection
