using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
  private Rigidbody myRigidbody;

  private void Awake()
  {
    myRigidbody = GetComponentInChildren<Rigidbody>();
  }

  public void Jump(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      Debug.Log("Jump");
      myRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
  }
}
