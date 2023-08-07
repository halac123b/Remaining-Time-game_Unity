using System.Collections;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
  // Start is called before the first frame update
  private void OnEnable()
  {
    StartCoroutine(Disable());
  }

  IEnumerator Disable()
  {
    yield return new WaitForSecondsRealtime(1);
    gameObject.SetActive(false);
  }
}
