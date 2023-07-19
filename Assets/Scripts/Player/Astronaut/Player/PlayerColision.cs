using UnityEngine;
using Unity.Netcode;

public class PlayerColision : NetworkBehaviour
{
  [SerializeField] private PlayerInput playerInput;
  [SerializeField] private PlayerMovement playerMovement;
  private bool isProcessing = false;
  private int processSpeed = 5;
  private void Awake()
  {
    playerInput = FindObjectOfType<PlayerInput>();
  }

  private void OnCollisionStay2D(Collision2D other)
  {
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

  private void OnCollisionExit2D(Collision2D other)
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
