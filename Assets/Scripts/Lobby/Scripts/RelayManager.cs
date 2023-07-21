using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using System;

public class RelayManager : NetworkBehaviour
{
  public static RelayManager Instance { get; private set; }
  //[SerializeField] private GameObject Loading;
  [SerializeField] private Material materialLoadding;

  private int clientId;

  private GameObject spawnObjTransform;

  public event EventHandler OnClientConnect;

  private void Awake()
  {
    Instance = this;
    clientId = 0;
    DontDestroyOnLoad(gameObject);
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

  public async Task<string> CreateRelay(PlayerData playerData)
  {
    try
    {
      Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
      string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      //StartCoroutine(ActivateObjectForDuration());
      NetworkManager.Singleton.StartHost();

      PointManager.Instance.SetPlayerData(clientId, playerData);
      clientId++;

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
      //StartCoroutine(ActivateObjectForDuration());
      NetworkManager.Singleton.StartClient();
      
      PointManager.Instance.SetPlayerData(Convert.ToInt32(OwnerClientId), playerData);
      Debug.Log("Client ID: "+ OwnerClientId);

      // SetPlayerDataServerRpc(playerData);
      StartCoroutine(Test(playerData));
    }

    catch (RelayServiceException e)
    {
      Debug.Log(e);
    }
  }

  IEnumerator Test(PlayerData playerData)
  {
    yield return new WaitForSeconds(3.45f);
    SetPlayerDataServerRpc(playerData);
    Debug.Log("client" + playerData.playerName);
  }

  // private IEnumerator ActivateObjectForDuration()
  // {

  //   materialLoadding.SetFloat("_Fade", 1f);
  //   Loading.SetActive(true);
  //   yield return new WaitForSeconds(3);
  //   while (materialLoadding.GetFloat("_Fade") > 0)
  //   {

  //     yield return new WaitForSeconds(Time.deltaTime);
  //     materialLoadding.SetFloat("_Fade", materialLoadding.GetFloat("_Fade") - 0.01f);
  //   }


  //   Loading.SetActive(false);
  // }

  [ServerRpc(RequireOwnership = false)]
  private void SetPlayerDataServerRpc(PlayerData playerData)
  {
    // SetPlayerDataClientRpc(playerData);
    PointManager.Instance.SetPlayerData(clientId, playerData);
    SetPlayerDataClientRpc(clientId, playerData);
    clientId++;
    OnClientConnect?.Invoke(this, EventArgs.Empty);
    Debug.Log("RPC2");
  }

  [ClientRpc]
  private void SetPlayerDataClientRpc(int index, PlayerData playerData)
  {
    if (!IsHost)
    {
      PointManager.Instance.SetPlayerData(index, playerData);
      Debug.Log("RPC3");
    }
  }
}
