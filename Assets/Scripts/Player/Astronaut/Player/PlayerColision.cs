using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerColision : NetworkBehaviour
{
  private PlayerInput playerInput;
  private PlayerMovement playerMovement;
  private bool isProcessing = false;
  private PlayerStatus playerStatus;

  private void Start()
  {
    playerInput = GetComponent<PlayerInput>();
    playerMovement = GetComponent<PlayerMovement>();
    playerStatus = FindAnyObjectByType<PlayerStatus>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!IsOwner || PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex == 0)
    {
      return;
    }
    if (other.gameObject.GetComponentInParent<OxyStatus>() != null)
    {
      FindObjectOfType<OxyGuide>().EnableTut();
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (!IsOwner)
    {
      return;
    }

    OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();

    if (oxy != null)
    {
      if (playerInput.GetIsProcessing())
      {
        if (!isProcessing)
        {
          FindObjectOfType<OxyGuide>().DisableTut();
          if (IsClient)
          {
            oxy.SetProcessServerRpc(true, playerStatus.processSpeed);
          }
          else
          {
            oxy.SetProcess(true, playerStatus.processSpeed);
          }

          isProcessing = true;
          playerMovement.SetCanMove(false);
        }
        else
        {
          FindObjectOfType<OxyGuide>().EnableTut();
          if (IsClient)
          {
            oxy.SetProcessServerRpc(false, -playerStatus.processSpeed);
          }
          else
          {
            oxy.SetProcess(false, -playerStatus.processSpeed);
          }
          isProcessing = false;
          playerMovement.SetCanMove(true);
        }
      }
    }
  }


  private void OnTriggerExit2D(Collider2D other)
  {

    OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();
    if (oxy != null)
    {
      FindObjectOfType<OxyGuide>().DisableTut();
      if (isProcessing)
      {
        isProcessing = false;
        playerMovement.SetCanMove(true);
        oxy.SetProcess(false, playerStatus.processSpeed);
      }
    }
  }
  public bool IsInProcessing()
  {
    return isProcessing;
  }
}
