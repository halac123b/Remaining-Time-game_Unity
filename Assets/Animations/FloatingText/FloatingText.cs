using System.Collections;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
  [SerializeField] private float delayTime;

  private void OnEnable()
  {
    StartCoroutine(Disable());
  }

  IEnumerator Disable()
  {
    yield return new WaitForSecondsRealtime(delayTime);
    gameObject.SetActive(false);
  }
}
