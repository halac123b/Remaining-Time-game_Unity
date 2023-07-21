using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Collections;

public class StandbyManager : SingletonNetwork<StandbyManager>
{

  private int numberConnected = 0;

  [SerializeField] TextMeshProUGUI[] playerName;
  [SerializeField] Image[] playerAvatar;
  [SerializeField] TextMeshProUGUI[] playerPoint;
  [SerializeField] Slider roundSlider;
  [SerializeField] TextMeshProUGUI roundText;

  [SerializeField] Sprite[] characterSprite;

  private PlayerData playerData;

  [SerializeField] int increaseAmount = 100;

  [SerializeField] private SceneName nextScene = SceneName.ShoppingPhase;

  public void Start()
  {
    if (!IsServer)
    {
      return;
    }
    if (LoadingSceneManager.Instance.GetRound() != 1)
    {
      PointManager.Instance.RollPlayerIndex();
    }

    PointManager.Instance.IncreasePointAll(increaseAmount);

    int numPlayer = LoadingSceneManager.Instance.GetNumPlayer();

    // FixedString128Bytes[] name = new FixedString128Bytes[numPlayer];
    // int[] index = new int[numPlayer];
    // int[] point = new int[numPlayer];
    // int[] rank = new int[numPlayer];

    // for (int i = 0; i < numPlayer; i++)
    // {
    //   name[i] = PointManager.Instance.playerPoint[i].playerData.playerName;
    //   index[i] = PointManager.Instance.playerPoint[i].playerIndex;
    //   point[i] = PointManager.Instance.playerPoint[i].point;
    //   rank[i] = PointManager.Instance.playerPoint[i].rank;
    // }

    PlayerPoint playerData = PointManager.Instance.playerPoint[0];

    UpdateUIClientRpc(0, playerData);

    if (numPlayer >= 2)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[1];
      UpdateUIClientRpc(1, data);
    }

    if (numPlayer >= 3)
    {
      PlayerPoint data = PointManager.Instance.playerPoint[2];
      UpdateUIClientRpc(2, data);
    }

    Invoke("LoadNextScene",5f);
  }

  private void LoadNextScene(){
      LoadingSceneManager.Instance.LoadScene(nextScene);
  }

  [ClientRpc]
  private void UpdateUIClientRpc(int index, PlayerPoint data)
  {
    playerName[index].text = data.playerData.playerName;

    if (0 == Convert.ToInt32(OwnerClientId))
    {
      playerName[index].color = Color.yellow;
    }

    playerAvatar[index].sprite = characterSprite[Mathf.Clamp(data.playerIndex, 0, 1)];

    playerPoint[index].text = data.point.ToString() + " $";
    if (data.rank == 1)
    {
      playerPoint[index].color = Color.red;
    }

    roundSlider.value = LoadingSceneManager.Instance.GetRound();
    roundText.text = roundSlider.value.ToString() + "/6";
  }


}


