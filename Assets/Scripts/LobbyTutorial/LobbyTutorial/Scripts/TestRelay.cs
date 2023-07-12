using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System.Threading.Tasks;

// public static TestRelay Instance { get; private set; }
public class TestRelay : MonoBehaviour
{
  public static TestRelay Instance { get; private set; }

  [SerializeField] private GameObject gameUI;
  [SerializeField] private PlayerStatus playerStatus;
  [SerializeField] private GameObject oxyBottle;
  private GameObject spawnObjTransform;

  private void Awake()
  {
    Instance = this;
  }

  // Start is called before the first frame update
  private async void Start()
  {
    await UnityServices.InitializeAsync();
    AuthenticationService.Instance.SignedIn += () =>
    {
      Debug.Log("Start TestRelay");
    };

    await AuthenticationService.Instance.SignInAnonymouslyAsync();
  }

  public async
Task<string>
CreateRelay()
  {
    try
    {
      Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
      string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

      NetworkManager.Singleton.StartHost();

      // Ha's test
      gameUI.SetActive(true);
      playerStatus.SetStartCounting(true);
      spawnObjTransform = Instantiate(oxyBottle);
      spawnObjTransform.GetComponent<NetworkObject>().Spawn(true);
      //

      return joinCode;
    }
    catch (RelayServiceException e)
    {
      Debug.Log(e);
      return null;
    }
  }


  public async void JoinRelay(string joinCode)
  {
    try
    {
      Debug.Log("Joining Relay with " + joinCode);
      JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

      NetworkManager.Singleton.StartClient();

      // Ha's test
      gameUI.SetActive(true);
      playerStatus.SetStartCounting(true);
      //
    }
    catch (RelayServiceException e)
    {
      Debug.Log(e);
    }
  }
}
