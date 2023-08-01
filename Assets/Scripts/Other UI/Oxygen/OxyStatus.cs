using System.Collections;
using UnityEngine;
using System;
using Unity.Netcode;

public class OxyStatus : NetworkBehaviour
{
  private NetworkVariable<int> process = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

  [SerializeField] private int threshold = 100;

  [SerializeField] private HealthBar processBar;

  private int speed = 5;

  private bool countTrigger = true;
  private bool startCounting = false;

  public event EventHandler<IntEventArg> OnProcessing;

  public event EventHandler OnOxyComplete;

  public class IntEventArg : EventArgs
  {
    public int value;
  }

  private void Start()
  {
    process.OnValueChanged += processBar.SetHealth;
  }
  public void Update()
  {
    if (countTrigger && startCounting && process.Value < threshold)
    {
      StartCoroutine(Processing());
    }
  }

  IEnumerator Processing()
  {
    countTrigger = false;
    yield return new WaitForSeconds(1);

    process.Value += speed;

    OnProcessing?.Invoke(this, new IntEventArg { value = process.Value });

    if (process.Value == threshold)
    {
      OnOxyComplete?.Invoke(this, EventArgs.Empty);
    }

    countTrigger = true;
  }

  public void SetProcess(bool status, int speed)
  {
    startCounting = status;
    this.speed += speed;
  }

  [ServerRpc(RequireOwnership = false)]
  public void SetProcessServerRpc(bool status, int speed)
  {
    startCounting = status;
    this.speed += speed;
  }
}
