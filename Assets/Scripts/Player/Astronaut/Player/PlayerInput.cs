using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : NetworkBehaviour
{
  public PlayerInputAction playerInputActions;
  private Item_Equip_Inventory inventory;
  private PlayerItem playerItem;
  private int TypeMove;
  private bool isProcessing = false;
  [SerializeField] CapsuleCollider2D capsuleCollider2D;
  [SerializeField] PlayerAnimator playerAnimator;

  private void Awake()
  {
    inventory =  FindAnyObjectByType<Item_Equip_Inventory>();  
    playerItem = FindAnyObjectByType<PlayerItem>();
    playerInputActions = new PlayerInputAction();
    playerInputActions.Player.Enable();
    playerInputActions.Player.Run.performed += Run;
    playerInputActions.Player.Duck.performed += Duck;

    playerInputActions.Player.Process.started += ProcessStarted;
    playerInputActions.Player.Process.performed += ProcessPerformed;
    playerInputActions.Player.Process.canceled += ProcessCanceled;

    playerInputActions.Player._1.performed += _1Use;
    playerInputActions.Player._2.performed += _2Use;
    playerInputActions.Player._3.performed += _3Use;
    playerInputActions.Player.tab.started += OpenInventory;
    playerInputActions.Player.tab.canceled += CloseInventory;
    
    

  }

  private void Start()
  {
    
  }

    private void CloseInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
      if(!IsOwner) return;
        // UnityEngine.Debug.LogError($"show = false");
        inventory.show_inventory = false;
        playerAnimator.canattack = true;


    }

    private void OpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
      if(!IsOwner) return;
        // UnityEngine.Debug.LogError($"show = true");

        inventory.show_inventory = true;
        playerAnimator.canattack = false;

    }
    private void _1Use(InputAction.CallbackContext context)
    {
      if(!IsOwner) return;
      if(inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item != null && playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item.GetName()) != null) playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item.GetName()).number -= 1; 
    }

    private void _2Use(InputAction.CallbackContext context)
    {
      if(!IsOwner) return;
      if(inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item != null && playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item.GetName()) != null) playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item.GetName()).number -= 1;   
    }

    private void _3Use(InputAction.CallbackContext context)
    {
      if(!IsOwner) return;
      if(inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item != null && playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item.GetName()) != null) playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item.GetName()).number -= 1; 
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
