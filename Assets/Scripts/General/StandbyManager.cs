using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StandbyManager : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI[] playerName;
  [SerializeField] Image[] playerAvatar;
  [SerializeField] TextMeshProUGUI[] playerPoint;

  [SerializeField] Sprite[] characterSprite;

  private PlayerData playerData;

  [SerializeField] int increaseAmount = 100;

  private void Start()
  {
    if (LoadingSceneManager.Instance.GetRound() != 1)
    {
      PointManager.Instance.RollPlayerIndex();
    }

    PointManager.Instance.IncreasePointAll(increaseAmount);

    for (int i = 0; i < LoadingSceneManager.Instance.GetNumPlayer(); i++)
    {
      playerName[i].text = PlayerStatus.Instance.GetPlayerData().playerName;
      playerAvatar[i].sprite = characterSprite[Mathf.Clamp(PointManager.Instance.playerPoint[i].playerIndex, 0, 1)];
      playerPoint[i].text = PointManager.Instance.playerPoint[i].point.ToString() + " $";
    }
  }
}
