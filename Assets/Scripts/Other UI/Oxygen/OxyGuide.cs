using UnityEngine;

public class OxyGuide : MonoBehaviour
{
  public void EnableTut()
  {
    transform.localScale = new Vector3(2, 2, 2);
  }

  public void DisableTut()
  {
    transform.localScale = Vector3.zero;
  }
}
