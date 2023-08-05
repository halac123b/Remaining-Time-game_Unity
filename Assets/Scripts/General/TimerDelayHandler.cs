using System.Collections;
using UnityEngine;

public class TimerDelayHandler : Singleton<TimerDelayHandler>
{
  public static void StartTimer(float delayTime, System.Action callback)
  {
    Instance.StartCoroutine(Instance.TimerCoroutine(delayTime, callback));
  }

  private IEnumerator TimerCoroutine(float delayTime, System.Action callback)
  {
    yield return new WaitForSeconds(delayTime);
    callback?.Invoke();
  }
}
