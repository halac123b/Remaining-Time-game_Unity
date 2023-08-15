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

  PlayerEquip playerEquip;
  PlayerStatus playerStatus;
  PlayerItem playerItem;

  [SerializeField] Button[] changeModeBtn;

  EquipmentInStore equipStore;

  [SerializeField] TextMeshProUGUI timeLeft;
  [SerializeField] Button sellTimeBtn;
  [SerializeField] GameObject timeSellField;
  [SerializeField] GameObject itemSellField;

  public enum Mode : byte
  {
    Buff,
    Weapon,
    Item,
    Life
  };

  private int lastMode = 0;

  private Mode storeMode = Mode.Weapon;

  private int currentSlot;

  int[] indexSlotItem = new int[4];

  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerEquip = FindObjectOfType<PlayerEquip>();
    playerItem = FindObjectOfType<PlayerItem>();
    equipStore = FindObjectOfType<EquipmentInStore>();
  }

  private void Start()
  {
    pointText.text = playerStatus.GetPoint().ToString();

    exitBtn.onClick.AddListener(Exit);
    navigationBtn[0].onClick.AddListener(NavigateLeft);
    navigationBtn[1].onClick.AddListener(NavigateRight);

    changeModeBtn[0].onClick.AddListener(delegate { ChangeMode(0); });
    changeModeBtn[1].onClick.AddListener(delegate { ChangeMode(1); });
    changeModeBtn[2].onClick.AddListener(delegate { ChangeMode(2); });
    changeModeBtn[3].onClick.AddListener(delegate { ChangeMode(3); });

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
    if (mode == 1)
    {
      storeMode = Mode.Weapon;
      if (lastMode == 3)
      {
        timeSellField.SetActive(false);
        itemSellField.SetActive(true);
      }
      ChangeColorMode(1);
      UpdateSlot();
    }
    else if (mode == 2)
    {
      if (lastMode == 3)
      {
        timeSellField.SetActive(false);
        itemSellField.SetActive(true);
      }
      storeMode = Mode.Item;
      ChangeColorMode(2);
      UpdateSlot();
    }
    else if (mode == 3)
    {
      if (lastMode != 3)
      {
        timeSellField.SetActive(true);
        itemSellField.SetActive(false);
      }
      storeMode = Mode.Life;
      ChangeColorMode(3);
    }

    else if (mode == 0)
    {
      if (lastMode == 3)
      {
        timeSellField.SetActive(false);
        itemSellField.SetActive(true);
      }
      storeMode = Mode.Buff;
      ChangeColorMode(0);
      UpdateSlot();
    }

    currentSlot = 0;
  }

  private void UpdateTime()
  {
    timeLeft.text = playerStatus.GetTimeLeft().ToString();

  }

  private void ChangeColorMode(int index)
  {
    changeModeBtn[lastMode].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    changeModeBtn[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    lastMode = index;
  }

  private void OnEnable()
  {
    storeMode = Mode.Buff;
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

    if ((storeMode == Mode.Weapon && currentSlot + 4 < playerEquip.equipmentList.Count) ||
      (storeMode == Mode.Item && currentSlot + 4 < playerItem.itemList.Count) ||
      (storeMode == Mode.Buff && currentSlot + 4 < playerStatus.buffList.Count))
    {
      navigationBtn[1].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[1].gameObject.SetActive(false);
    }

    int itemIndex = -1;

    for (int i = 0; i < 4; i++)
    {
      if ((storeMode == Mode.Weapon && i + currentSlot >= playerEquip.equipmentList.Count) ||
        (storeMode == Mode.Item && (i + currentSlot >= playerItem.itemList.Count)) ||
        (storeMode == Mode.Buff && i + currentSlot >= playerStatus.buffList.Count))
      {
        sprite[i].sprite = alpha;
        price[i].text = "";
        numberItem[i].text = "";

        indexSlotItem[i] = -1;
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
          int index = itemIndex;
          for (int j = itemIndex + 1; j < playerItem.itemList.Count; j++)
          {
            if (playerItem.itemList[j].number > 0)
            {
              index = j;
              break;
            }
          }

          if (index == itemIndex)
          {
            sprite[i].sprite = alpha;
            price[i].text = "";
            numberItem[i].text = "";

            indexSlotItem[i] = -1;
          }
          else
          {
            indexSlotItem[i] = index;
            sprite[i].sprite = playerItem.itemList[index].item.GetSprite();
            price[i].text = Convert.ToInt32(playerItem.itemList[index].item.price * 0.7f).ToString() + "$";
            numberItem[i].text = playerItem.itemList[index].number.ToString();
            itemIndex = index;
          }
        }

        else if (storeMode == Mode.Buff)
        {
          sprite[i].sprite = playerStatus.buffList[currentSlot + i].image;
          price[i].text = Convert.ToInt32(playerStatus.buffList[currentSlot + i].price * 0.7f).ToString() + "$";
          numberItem[i].text = "";
        }
      }
    }
  }


  private void Exit()
  {
    playerStatus.canMove = true;
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
    }
    else if (storeMode == Mode.Item)
    {
      if (indexSlotItem[index] == -1)
      {
        return;
      }
      playerStatus.SetPoint(playerStatus.GetPoint() + Convert.ToInt32(playerItem.itemList[indexSlotItem[index]].item.price * 0.7f));

      playerItem.itemList[indexSlotItem[index]].number--;

      pointText.text = playerStatus.GetPoint().ToString();
    }
    else if (storeMode == Mode.Buff)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() + Convert.ToInt32(playerStatus.buffList[currentSlot + index].price * 0.7f));

      playerStatus.buffList[currentSlot + index].DeActivate(playerStatus);
      playerStatus.buffList.RemoveAt(currentSlot + index);

      pointText.text = playerStatus.GetPoint().ToString();
    }

    UpdateSlot();
  }
}
