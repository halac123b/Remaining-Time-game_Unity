using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{

  [SerializeField] GameObject infoTab;

  public void EnableTab(bool state)
  {
    infoTab.SetActive(state);
  }
}
