using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using UnityEngine.UI;

public class ResultPhaseManager : SingletonNetwork<ResultPhaseManager>
{
  private int numberConnected = 0;

  [SerializeField] CountDown countDown;

  [SerializeField] private SceneName nextScene = SceneName.StandbyPhase;

  [SerializeField] private TextMeshProUGUI sceneName;

  [SerializeField] private Image[] playerAvatar;
  [SerializeField] private Sprite[] characterSprite;

  [SerializeField] private TextMeshProUGUI[] playerName;

  [SerializeField] private TextMeshProUGUI[] playerPoint;

  [SerializeField] private TextMeshProUGUI[] resultText;

  [SerializeField] private Image secondPos;
  [SerializeField] private Sprite rankIcon;

  public void ServerSceneInit()
  {
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

  private void Start()
  {
    if (!IsHost)
    {
      return;
    }
    countDown.OnTimeOut += LoadNextScene;

    bool dualWin = false;

    PlayerPoint playerData;
    for (int i = 0; i < 3; i++)
    {
      playerData = PointManager.Instance.playerPoint[i];

      if (dualWin && playerData.roundRank == 0)
      {
        UpdateUIClientRpc(1, playerData, true);
      }
      else
      {
        UpdateUIClientRpc(playerData.roundRank, playerData);
      }
      if (playerData.roundRank == 0 && !dualWin)
      {
        dualWin = true;
      }
    }
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.GoNextRound();

    if (LoadingSceneManager.Instance.GetRound() == 6)
    {
      nextScene = SceneName.Menu;
      NetworkManager.Singleton.Shutdown();
    }
    LoadingSceneManager.Instance.LoadScene(nextScene);

  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data, bool dualWin = false)
  {
    sceneName.text = LoadingSceneManager.Instance.GetRound().ToString();

    playerName[index].text = data.playerData.playerName;

    if (data.playerData.Id == NetworkManager.Singleton.LocalClientId)
    {
      playerName[index].color = Color.yellow;
    }

    playerAvatar[index].sprite = characterSprite[Mathf.Clamp(data.playerIndex, 0, 1)];

    playerPoint[index].text = data.point.ToString() + " $";

    if (data.rank == 1)
    {
      playerPoint[index].color = Color.red;
    }

    if (index == 0)
    {
      resultText[index].text = "+" + data.bidAmount.ToString() + " $";
    }
    else if (index == 1 && data.roundRank == 0)
    {
      resultText[index].text = "+" + data.bidAmount.ToString() + " $";
      secondPos.sprite = rankIcon;
    }
    else if (index == 2)
    {
      resultText[index].text = "-" + data.bidAmount.ToString() + " $";
    }
  }
}
