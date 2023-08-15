using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
  [SerializeField] private GameObject tutorialTab;

  // Update is called once per frame
  void Update()
  {
     if (tutorialTab != null)tutorialTab.SetActive(Input.GetKey(KeyCode.T));
  }
}
