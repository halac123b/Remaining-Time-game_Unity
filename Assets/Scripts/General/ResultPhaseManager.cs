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

    Debug.Log("Test1");
    ArrangeRank();

    bool dualWin = false;

    PlayerPoint playerData;

    int round = LoadingSceneManager.Instance.GetRound();

    for (int i = 0; i < 3; i++)
    {
      playerData = PointManager.Instance.playerPoint[i];

      if (dualWin && playerData.roundRank == 0)
      {
        UpdateUIClientRpc(1, playerData, round);
      }
      else
      {
        UpdateUIClientRpc(playerData.roundRank, playerData, round);
      }
      if (playerData.roundRank == 0 && !dualWin)
      {
        dualWin = true;
      }
    }
  }

  private void ArrangeRank()
  {
    int[] arr = new int[3];
    for (int i = 0; i < 3; i++)
    {
      arr[i] = PointManager.Instance.playerPoint[i].point;
    }

    Array.Sort(arr);

    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        if (PointManager.Instance.playerPoint[i].point == arr[j])
        {
          PointManager.Instance.playerPoint[i].rank = 2 - j;
          arr[j] = -1;
        }
      }
    }
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.GoNextRound();

    if (LoadingSceneManager.Instance.GetRound() == 2)
    {
      nextScene = SceneName.SummaryPhase;
    }
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data, int round)
  {
    sceneName.text = round.ToString();

    playerName[index].text = data.playerData.playerName;

    if (data.playerData.Id == NetworkManager.Singleton.LocalClientId)
    {
      playerName[index].color = Color.yellow;
    }

    playerAvatar[index].sprite = characterSprite[Mathf.Clamp(data.playerIndex, 0, 1)];

    playerPoint[index].text = data.point.ToString() + " $";

    if (data.rank == 0)
    {
      playerPoint[index].color = Color.red;
    }
    else
    {
      playerPoint[index].color = Color.white;
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
