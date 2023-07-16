using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
  public PlayerInputAction playerInputActions;
  private int TypeMove;
  private bool isProcessing = false;
  [SerializeField] CapsuleCollider2D capsuleCollider2D;

  private void Awake()
  {
   
    playerInputActions = new PlayerInputAction();
    playerInputActions.Player.Enable();
    playerInputActions.Player.Run.performed += Run;
    playerInputActions.Player.Duck.performed += Duck;

    playerInputActions.Player.Process.started += ProcessStarted;
    playerInputActions.Player.Process.performed += ProcessPerformed;
    playerInputActions.Player.Process.canceled += ProcessCanceled;

    // playerInputActions.Player.Attack.performed += AttackPerformed;
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;
    return inputVector;
  }

  public bool GetIsProcessing()
  {
    return isProcessing;
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


  private void ProcessStarted(InputAction.CallbackContext context)
  {
    isProcessing = true;
  }

  IEnumerator CoutDownTrigger()
  {
    yield return new WaitForFixedUpdate();
    isProcessing = false;
  }

  private void ProcessPerformed(InputAction.CallbackContext context)
  {
    StartCoroutine(CoutDownTrigger());
  }

  private void ProcessCanceled(InputAction.CallbackContext context)
  {
    StopAllCoroutines();
    isProcessing = false;
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
