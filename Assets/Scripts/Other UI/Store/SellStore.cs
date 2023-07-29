using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SellStore : MonoBehaviour
{
  [SerializeField] Button exitBtn;

  [SerializeField] Image[] sprite;
  [SerializeField] TextMeshProUGUI[] price;
  [SerializeField] TextMeshProUGUI[] numberItem;
  [SerializeField] Sprite alpha;

  [SerializeField] Button[] navigationBtn;

  [SerializeField] Button[] buyBtn;

  [SerializeField] TextMeshProUGUI pointText;

  [SerializeField] PlayerEquip playerEquip;
  [SerializeField] PlayerStatus playerStatus;
  [SerializeField] PlayerItem playerItem;

  [SerializeField] Button[] changeModeBtn;

  [SerializeField] EquipmentInStore equipStore;

  [SerializeField] TextMeshProUGUI timeLeft;
  [SerializeField] Button sellTimeBtn;
  [SerializeField] GameObject timeSellField;
  [SerializeField] GameObject itemSellField;

  public enum Mode : byte
  {
    Weapon,
    Item,
    Life
  };

  private int lastMode = 0;

  private Mode storeMode = Mode.Weapon;

  private int currentSlot;

  private void Start()
  {
    pointText.text = playerStatus.GetPoint().ToString();

    exitBtn.onClick.AddListener(Exit);
    navigationBtn[0].onClick.AddListener(NavigateLeft);
    navigationBtn[1].onClick.AddListener(NavigateRight);

    changeModeBtn[0].onClick.AddListener(delegate { ChangeMode(0); });
    changeModeBtn[1].onClick.AddListener(delegate { ChangeMode(1); });
    changeModeBtn[2].onClick.AddListener(delegate { ChangeMode(2); });

    buyBtn[0].onClick.AddListener(delegate { SellEquip(0); });
    buyBtn[1].onClick.AddListener(delegate { SellEquip(1); });
    buyBtn[2].onClick.AddListener(delegate { SellEquip(2); });
    buyBtn[3].onClick.AddListener(delegate { SellEquip(3); });

    sellTimeBtn.onClick.AddListener(SellTime);
    UpdateTime();
  }

  private void SellTime()
  {
    playerStatus.SetTimeLeft(playerStatus.GetTimeLeft() - 5);
    playerStatus.SetPoint(playerStatus.GetPoint() + 5);
    pointText.text = playerStatus.GetPoint().ToString();
    UpdateTime();
  }

  private void ChangeMode(int mode)
  {
    if (mode == 0)
    {
      storeMode = Mode.Weapon;
      if (lastMode == 2)
      {
        timeSellField.SetActive(false);
        itemSellField.SetActive(true);
      }
      ChangeColorMode(0);
      UpdateSlot();
    }
    else if (mode == 1)
    {
      if (lastMode == 2)
      {
        timeSellField.SetActive(false);
        itemSellField.SetActive(true);
      }
      storeMode = Mode.Item;
      ChangeColorMode(1);
      UpdateSlot();
    }
    else if (mode == 2)
    {
      if (lastMode != 2)
      {
        timeSellField.SetActive(true);
        itemSellField.SetActive(false);
      }
      storeMode = Mode.Life;
      ChangeColorMode(2);
    }

    currentSlot = 0;
  }

  private void UpdateTime()
  {
    timeLeft.text = playerStatus.GetTimeLeft().ToString();

  }

  private void ChangeColorMode(int index)
  {
    changeModeBtn[lastMode].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    changeModeBtn[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    lastMode = index;
  }

  private void OnEnable()
  {
    storeMode = Mode.Weapon;
    currentSlot = 0;
    UpdateSlot();
    pointText.text = playerStatus.GetPoint().ToString();
  }

  private void UpdateSlot()
  {
    if (currentSlot > 0)
    {
      navigationBtn[0].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[0].gameObject.SetActive(false);
    }

    if ((storeMode == Mode.Weapon && currentSlot + 4 <= playerEquip.equipmentList.Count) || (storeMode == Mode.Item && currentSlot + 4 <= playerItem.itemList.Count))
    {
      navigationBtn[1].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[1].gameObject.SetActive(false);
    }

    for (int i = 0; i < 4; i++)
    {
      if ((storeMode == Mode.Weapon && i + currentSlot >= playerEquip.equipmentList.Count) || (storeMode == Mode.Item && i + currentSlot >= playerItem.itemList.Count))
      {
        sprite[i].sprite = alpha;
        price[i].text = "";
        numberItem[i].text = "";
      }

      else
      {
        if (storeMode == Mode.Weapon)
        {
          sprite[i].sprite = playerEquip.equipmentList[currentSlot + i].GetSprite();
          price[i].text = Convert.ToInt32(playerEquip.equipmentList[currentSlot + i].GetPrice() * 0.7f).ToString() + "$";
          numberItem[i].text = "";
        }

        else if (storeMode == Mode.Item)
        {
          sprite[i].sprite = playerItem.itemList[currentSlot + i].item.GetSprite();
          price[i].text = Convert.ToInt32(playerItem.itemList[currentSlot + i].item.price * 0.7f).ToString() + "$";
        }
      }
    }
  }


  private void Exit()
  {
    gameObject.SetActive(false);
  }

  private void NavigateLeft()
  {
    currentSlot -= 4;
    UpdateSlot();
  }

  private void NavigateRight()
  {
    currentSlot += 4;
    UpdateSlot();
  }

  private void SellEquip(int index)
  {
    if ((storeMode == Mode.Weapon && currentSlot + index > playerEquip.equipmentList.Count) || (storeMode == Mode.Item && currentSlot + index > playerItem.itemList.Count))
    {
      return;
    }

    if (storeMode == Mode.Weapon)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() + Convert.ToInt32(playerEquip.equipmentList[currentSlot + index].GetPrice() * 0.7f));
      equipStore.equipmentList.Add(playerEquip.equipmentList[currentSlot + index]);
      playerEquip.equipmentList.RemoveAt(currentSlot + index);

      pointText.text = playerStatus.GetPoint().ToString();
      UpdateSlot();
    }
    else if (storeMode == Mode.Item)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() + Convert.ToInt32(playerItem.itemList[currentSlot + index].item.price * 0.7f));

      playerItem.itemList[currentSlot + index].number--;
      if (playerItem.itemList[currentSlot + index].number == 0)
      {
        playerItem.itemList.RemoveAt(currentSlot + index);
      }
      pointText.text = playerStatus.GetPoint().ToString();
    }
  }
}
