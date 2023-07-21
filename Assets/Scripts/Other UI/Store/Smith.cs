using UnityEngine;
using Unity.Netcode;
using System;

public class Smith : NetworkBehaviour
{
  [SerializeField] GameObject buttonUI;

  private void OnTriggerEnter2D(Collider2D other)
  {
    PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    if (playerMovement != null)
    {
      Debug.Log(OwnerClientId);
      if (other.gameObject.GetComponent<PlayerMovement>().GetClientId() == Convert.ToInt32(OwnerClientId))
      {
        buttonUI.SetActive(true);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    if (playerMovement != null)
    {
      if (other.gameObject.GetComponent<PlayerMovement>().GetClientId() == Convert.ToInt32(OwnerClientId))
      {
        buttonUI.SetActive(false);
      }
    }
  }
}
