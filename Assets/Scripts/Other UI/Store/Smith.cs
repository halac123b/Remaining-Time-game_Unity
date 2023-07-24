using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class Smith : NetworkBehaviour
{
  [SerializeField] GameObject buttonUI;
  [SerializeField] GameObject shopUI;

  private bool interactive = false;

  [SerializeField] private List<EquipmentSO> equipmentList = new List<EquipmentSO>();

  private void OnTriggerEnter2D(Collider2D other)
  {
    PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    if (playerMovement != null)
    {
      if (playerMovement.GetClientId() == NetworkManager.Singleton.LocalClientId)
      {
        buttonUI.SetActive(true);
        interactive = true;
      }
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
    if (playerMovement != null)
    {
      if (playerMovement.GetClientId() == NetworkManager.Singleton.LocalClientId)
      {
        buttonUI.SetActive(false);
        interactive = false;
      }
    }
  }

  private void Update()
  {
    if (interactive)
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        shopUI.SetActive(true);
      }
    }
  }
}
