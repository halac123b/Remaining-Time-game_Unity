using UnityEngine;
using Unity.Netcode;

public class MainPhaseManager : SingletonNetwork<MainPhaseManager>
{
  private int numberConnected = 0;

  [SerializeField] CountDown countDown;

  public void ServerSceneInit()
  {
    Debug.Log("I'm here");
    numberConnected++;

    // Check if is the last client
    if (numberConnected != LoadingSceneManager.Instance.GetNumPlayer())
      return;


    StartCountClientRpc();
  }

  [ClientRpc]
  private void StartCountClientRpc()
  {
    countDown.SetStartCounting();
  }
}
