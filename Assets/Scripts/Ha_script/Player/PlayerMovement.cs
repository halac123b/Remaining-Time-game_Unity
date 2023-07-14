using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  [SerializeField] private float moveSpeed = 7f;
  private GameInput gameInput;

  private Vector2 walkVector;
  private Vector2 lastDirection;

  private bool canMove = true;
  private bool isProcessing = false;

  [SerializeField] int processSpeed = 5;

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

    if (canMove)
    {
      HandleMovement();
    }
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

  private void OnCollisionStay2D(Collision2D other)
  {
    Debug.Log(1111);
    OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();
    if (oxy != null)
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        if (!isProcessing)
        {
          oxy.SetProcess(true, processSpeed);
          isProcessing = true;
          canMove = false;
        }
        else
        {
          isProcessing = false;
          canMove = true;
        }
      }
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.GetComponent<OxyStatus>() != null)
    {
      if (isProcessing)
      {
        isProcessing = false;
        canMove = true;
      }
    }
  }

  public bool GetProcessStatus()
  {
    return isProcessing;
  }
}

// Fix: Remove LastDirection
