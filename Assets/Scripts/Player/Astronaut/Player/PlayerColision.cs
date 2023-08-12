using UnityEngine;
using Unity.Netcode;

public class PlayerColision : NetworkBehaviour
{
  private PlayerInput playerInput;
  private PlayerMovement playerMovement;
  private bool isProcessing = false;
  private int processSpeed = 5;

  private void Start()
  {
    playerInput = GetComponent<PlayerInput>();
    playerMovement = GetComponent<PlayerMovement>();
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
          if (IsClient)
          {
            oxy.SetProcessServerRpc(true, processSpeed);
          }
          else
          {
            oxy.SetProcess(true, processSpeed);
          }

          isProcessing = true;
          playerMovement.SetCanMove(false);
        }
        else
        {
          if (IsClient)
          {
            oxy.SetProcessServerRpc(false, -processSpeed);
          }
          else
          {
            oxy.SetProcess(false, -processSpeed);
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
      if (isProcessing)
      {
        isProcessing = false;
        playerMovement.SetCanMove(true);
        oxy.SetProcess(false, processSpeed);
      }
    }
  }
  public bool IsInProcessing()
  {
    return isProcessing;
  }
}
