using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using System;
// using Microsoft.Unity.VisualStudio.Editor;

// public static TestRelay Instance { get; private set; }
public class RelayManager : MonoBehaviour
{
  public static RelayManager Instance { get; private set; }
  [SerializeField] private GameObject Loading;
  [SerializeField] private Material materialLoadding;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private PlayerStatus playerStatus;

  [SerializeField] private GameObject oxyBottle;
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject monsterPrefab;

  private GameObject spawnObjTransform;

  private void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  // private async void Start()
  // {
  //   await UnityServices.InitializeAsync();
  //   AuthenticationService.Instance.SignedIn += () =>
  //   {
  //     Debug.Log("Start TestRelay");
  //   };

  //   // await AuthenticationService.Instance.SignInAnonymouslyAsync();
  // }

  public async
Task<string>
CreateRelay(PlayerData playerData)
  {
    try
    {
      Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
      string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      StartCoroutine(ActivateObjectForDuration());
      NetworkManager.Singleton.StartHost();

      // Ha's test
      gameUI.SetActive(true);
      playerStatus.SetPlayerData(playerData);
      playerStatus.SetStartCounting(true);

      // Spawn oxygen
      spawnObjTransform = Instantiate(oxyBottle);
      spawnObjTransform.GetComponent<NetworkObject>().Spawn(true);

      // Player
      spawnObjTransform = Instantiate(playerPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
      spawnObjTransform.GetComponent<NetworkObject>().Spawn(true);

      spawnObjTransform = Instantiate(playerPrefab, new Vector3(10, 0, 0), Quaternion.identity);
      spawnObjTransform.GetComponent<NetworkObject>().SpawnWithOwnership(1);

      Debug.Log("Find id" + NetworkManager.Singleton.ConnectedClientsIds.Count);

      return joinCode;
    }
    catch (RelayServiceException e)
    {
      Debug.Log(e);
      return null;
    }
  }

  public async void JoinRelay(string joinCode, PlayerData playerData)
  {
    try
    {
      Debug.Log("Joining Relay with " + joinCode);
      JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      StartCoroutine(ActivateObjectForDuration());
      NetworkManager.Singleton.StartClient();

      // Ha's test
      playerStatus.SetPlayerData(playerData);
      gameUI.SetActive(true);
      playerStatus.SetStartCounting(true);

    }
    catch (RelayServiceException e)
    {
      Debug.Log(e);
    }
  }

  private IEnumerator ActivateObjectForDuration()
  {

    materialLoadding.SetFloat("_Fade", 1f);
    Loading.SetActive(true);
    yield return new WaitForSeconds(3);
    while (materialLoadding.GetFloat("_Fade") > 0)
    {

      yield return new WaitForSeconds(Time.deltaTime);
      materialLoadding.SetFloat("_Fade", materialLoadding.GetFloat("_Fade") - 0.01f);
    }


    Loading.SetActive(false);
  }
}
