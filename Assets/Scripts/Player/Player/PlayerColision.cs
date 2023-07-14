using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerColision : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool isProcessing = false;
    private bool canMove = true;
    private bool TriggerPoccessing = false;
    private int processSpeed = 5;
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public bool CanMove(){
        return canMove;
    }

    public void triggerPoccessing(){
        TriggerPoccessing = true;
    }
      private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log(1111);
        OxyStatus oxy = other.gameObject.GetComponentInParent<OxyStatus>();
        if (oxy != null)
        {
            if (TriggerPoccessing){

                // Debug.Log("OK");
                if (!isProcessing)
                {
                    oxy.SetProcess(true, processSpeed);
                    isProcessing = true;
                    canMove = false;
                }
                else
                {
                    oxy.SetProcess(false, processSpeed);
                    isProcessing = false;
                    canMove = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        Debug.Log("Exit trigger");
        if (other.gameObject.GetComponent<OxyStatus>() != null)
        {
        if (isProcessing)
        {
            isProcessing = false;
            canMove = true;
            other.gameObject.GetComponent<OxyStatus>().SetProcess(false, processSpeed);
        }
        }
    }


    public bool IsInProcessing()
    {
        return isProcessing;
    }
    }
