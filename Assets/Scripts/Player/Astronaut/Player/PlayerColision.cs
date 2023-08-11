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
    Debug.Log("name " + other.gameObject.name);

    OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();
    Debug.Log("zzzz1 " + oxy);
    if (oxy != null)
    {
      Debug.Log("aaa " + playerInput.GetIsProcessing());
      if (playerInput.GetIsProcessing())
      {
        if (!isProcessing)
        {
          Debug.Log("isclient " + IsClient);
          if (IsClient)
          {
            oxy.SetProcessServerRpc(true, processSpeed);
          }
          else
          {
            Debug.Log("fafsaf");
            oxy.SetProcess(true, processSpeed);
          }

          isProcessing = true;
          Debug.Log("propro " + isProcessing);
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
          Debug.Log("222 " + isProcessing + "  " + gameObject.name);
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
        Debug.Log("namexxx " + other.gameObject.name);
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

  private void Update()
  {
    Debug.Log("process " + isProcessing);
  }
}
