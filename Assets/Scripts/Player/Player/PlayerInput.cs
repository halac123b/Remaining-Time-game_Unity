using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
  private PlayerInputAction playerInputActions;
  private int TypeMove;
  [SerializeField] CapsuleCollider2D capsuleCollider2D;
  private void Awake()
  {
    playerInputActions = new PlayerInputAction();
    playerInputActions.Player.Enable();
    playerInputActions.Player.Run.performed += Run;
    playerInputActions.Player.Duck.performed += Duck;
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;
    return inputVector;
  }

  public int GetTypeMove()
  {
    return TypeMove;
  }
  private void Run(InputAction.CallbackContext context)
  {
    if (context.ReadValueAsButton())
    {
      TypeMove = 1;
    }
    else TypeMove = 0;
  }
  private void Duck(InputAction.CallbackContext context)
  {
    if (context.ReadValueAsButton())
    {
      TypeMove = -1;
      capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, capsuleCollider2D.offset.y - 0.2f);
      capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, capsuleCollider2D.size.y - 0.4f);
    }
    else
    {
      TypeMove = 0;
      capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, capsuleCollider2D.offset.y + 0.2f);
      capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, capsuleCollider2D.size.y + 0.4f);
    }
  }
}
