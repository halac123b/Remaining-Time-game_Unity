using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
  private float moveSpeed = 5f;
  private PlayerInput playerInput;
  private PlayerColision playerColision;

  private Vector2 moveVector;
  private Vector2 lastDirection;

  // private bool canMove = true;
  // private bool isProcessing = false;

  // [SerializeField] int processSpeed = 5;

  private void Awake()
  {
    lastDirection = new Vector2(0, 1);
    playerInput = FindObjectOfType<PlayerInput>();
    playerColision = FindObjectOfType<PlayerColision>();
  }

  private void Update()
  {
    if (!IsOwner)
    {
      return;
    }
    if (playerColision.CanMove())
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

  //   private void OnTriggerStay2D(Collider2D other)
  // {
  //   // Debug.Log(1111);
  //   OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();
  //   if (oxy != null)
  //   {
  //   if (playerInput.GetIsProcessing()){
  //     Debug.Log("M vua nhan E dung khong");
  //       {
  //         Debug.Log(1222111);
  //         if (!isProcessing)
  //         {
  //           oxy.SetProcess(true, processSpeed);
  //           isProcessing = true;
  //           canMove = false;
  //         }
  //         else
  //         {
  //           oxy.SetProcess(false, processSpeed);
  //           isProcessing = false;
  //           canMove = true;
  //         }
  //       }
  //     }
  //   }
  // }


  // private void OnTriggerExit2D(Collider2D other)
  // {

  //   Debug.Log("Exit trigger");
  //   if (other.gameObject.GetComponent<OxyStatus>() != null)
  //   {
  //     if (isProcessing)
  //     {
  //       isProcessing = false;
  //       canMove = true;
  //       other.gameObject.GetComponent<OxyStatus>().SetProcess(false, processSpeed);
  //     }
  //   }
  // }

  public bool GetProcessStatus()
  {
    return playerColision.IsInProcessing();
  }
}

// Fix: Remove LastDirection
