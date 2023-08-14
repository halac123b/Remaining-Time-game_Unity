using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SummaryPhaseManager : SingletonNetwork<ResultPhaseManager>
{
  private int numberConnected = 0;

  [SerializeField] private TextMeshProUGUI[] playerName;

  [SerializeField] private TextMeshProUGUI[] playerPoint;

  [SerializeField] private Button newGameBtn;

  public void ServerSceneInit()
  {
    numberConnected++;

    // Check if is the last client
    if (numberConnected != LoadingSceneManager.Instance.GetNumPlayer())
      return;
  }

  private void Start()
  {
    newGameBtn.onClick.AddListener(BackToMenu);
    if (!IsHost)
    {
      return;
    }

    PlayerPoint playerData;

    for (int i = 0; i < 3; i++)
    {
      playerData = PointManager.Instance.playerPoint[i];

      UpdateUIClientRpc(playerData.rank, playerData);
    }

    StartCoroutine(ShutDownServer());
  }

  IEnumerator ShutDownServer()
  {
    yield return new WaitForSecondsRealtime(3f);

    NetworkManager.Singleton.Shutdown();
  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data)
  {
    playerName[index].text = data.playerData.playerName;

    if (data.playerData.Id == NetworkManager.Singleton.LocalClientId)
    {
      playerName[index].color = Color.yellow;
    }

    playerPoint[index].text = data.point.ToString() + " $";
  }

  private void BackToMenu()
  {
    // PointManager.Instance.gameObject.GetComponent<NetworkObject>().Despawn();
    Destroy(PointManager.Instance.gameObject);

    Destroy(FindObjectOfType<PlayerStatus>().gameObject);

    LoadingSceneManager.Instance.RefreshGame();
    LoadingSceneManager.Instance.LoadScene(SceneName.Menu);
  }
}
