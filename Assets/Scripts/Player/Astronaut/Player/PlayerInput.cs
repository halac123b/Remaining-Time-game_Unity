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
  private InfoUI infoTab;

  private PlayerItem playerItem;
  private int TypeMove;
  private bool isProcessing = false;
  [SerializeField] CapsuleCollider2D capsuleCollider2D;

  private PlayerStatus playerStatus;

  private void Awake()
  {
    inventory = FindAnyObjectByType<Item_Equip_Inventory>();
    playerItem = FindAnyObjectByType<PlayerItem>();
    playerStatus = FindObjectOfType<PlayerStatus>();
    infoTab = FindObjectOfType<InfoUI>();

    playerInputActions = new PlayerInputAction();
    playerInputActions.Player.Enable();
    playerInputActions.Player.Run.performed += Run;
    playerInputActions.Player.Duck.performed += Duck;

    playerInputActions.Player.Process.started += ProcessStarted;
    //playerInputActions.Player.Process.performed += ProcessPerformed;
    playerInputActions.Player.Process.canceled += ProcessCanceled;

    playerInputActions.Player._1.performed += _1Use;
    playerInputActions.Player._2.performed += _2Use;
    playerInputActions.Player._3.performed += _3Use;
    playerInputActions.Player.tab.started += OpenInventory;
    playerInputActions.Player.tab.canceled += CloseInventory;
    playerInputActions.Player.Info.started += OpenInfo;
    playerInputActions.Player.Info.canceled += CloseInfo;
  }

  private void CloseInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    // UnityEngine.Debug.LogError($"show = false");
    inventory.show_inventory = false;
    playerStatus.canattack = true;
  }

  private void OnDisable()
  {
    playerInputActions.Player.Disable();
  }

  private void OpenInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    // UnityEngine.Debug.LogError($"show = true");

    inventory.show_inventory = true;
    playerStatus.canattack = false;
  }

  private void OpenInfo(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    infoTab.EnableTab(true);
  }

  private void CloseInfo(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    infoTab.EnableTab(false);
  }

  private void _1Use(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    if (inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item != null &&
        playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item.GetName()) != null)
    {
      playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(2).item.GetName()).number -= 1;
    }
  }

  private void _2Use(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    if (inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item != null && playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item.GetName()) != null)
    {
      playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(1).item.GetName()).number -= 1;
    }
  }

  private void _3Use(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    if (inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item != null && playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item.GetName()) != null)
    {
      playerItem.GetItem(inventory.GetComponentInChildren<InventoryUI>().GetInventory(0).item.GetName()).number -= 1;
    }
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
    if (!IsOwner) return;
    isProcessing = true;
    UnityEngine.Debug.Log("ProcessON");

    StartCoroutine(CoutDownTrigger());
  }

  IEnumerator CoutDownTrigger()
  {
    yield return new WaitForFixedUpdate();

    UnityEngine.Debug.Log("now");
    isProcessing = false;
  }

  // private void ProcessPerformed(InputAction.CallbackContext context)
  // {


  // }

  private void ProcessCanceled(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    // StopAllCoroutines();
    isProcessing = false;

    UnityEngine.Debug.Log("won");
  }

  private void Duck(InputAction.CallbackContext context)
  {
    if (context.ReadValueAsButton())
    {
      TypeMove = -1;
      // capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, capsuleCollider2D.offset.y - 0.2f);
      // capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, capsuleCollider2D.size.y - 0.4f);
    }
    else
    {
      TypeMove = 0;
      // capsuleCollider2D.offset = new Vector2(capsuleCollider2D.offset.x, capsuleCollider2D.offset.y + 0.2f);
      // capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, capsuleCollider2D.size.y + 0.4f);
    }
  }
}
