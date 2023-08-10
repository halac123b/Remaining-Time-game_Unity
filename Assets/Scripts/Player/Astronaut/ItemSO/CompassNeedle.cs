using UnityEngine;

public class CompassNeedle : MonoBehaviour
{
  private Transform oxy;

  private void OnEnable()
  {
    oxy = FindObjectOfType<OxyStatus>().transform;
  }

  private float rotateSpeed = 3f;
  private void Update()
  {
    transform.up = Vector3.Slerp(transform.up, oxy.position, Time.deltaTime * rotateSpeed);
    Debug.Log("vv " + oxy.position);
  }
}
