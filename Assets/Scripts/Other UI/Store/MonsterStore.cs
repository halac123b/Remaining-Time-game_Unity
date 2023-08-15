using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterStore : MonoBehaviour
{
  [SerializeField] private List<BuffSO> buffList = new List<BuffSO>();

  [SerializeField] Button exitBtn;

  private EquipmentInStore equipmentStock;

  [SerializeField] Image[] sprite;

  [SerializeField] TextMeshProUGUI nameEquip;
  [SerializeField] TextMeshProUGUI[] price;
  [SerializeField] Sprite alpha;

  [SerializeField] Button[] navigationBtn;
  [SerializeField] Button[] itemSlotBtn;

  [SerializeField] Button[] buyBtn;
  [SerializeField] TextMeshProUGUI pointText;

  PlayerStatus playerStatus;

  [SerializeField] TextMeshProUGUI itemDescription;

  [SerializeField] Button[] changeModeBtn;

  public enum Mode : byte
  {
    Buff,
    Skill,
    Miniboss
  };

  private Mode storeMode = Mode.Buff;

  private int currentSlot;
  private int lastActiveEquip = 0;

  private void Awake()
  {
    equipmentStock = FindObjectOfType<EquipmentInStore>();
    playerStatus = FindObjectOfType<PlayerStatus>();
  }

  private void Start()
  {
    pointText.text = playerStatus.GetPoint().ToString();

    exitBtn.onClick.AddListener(Exit);
    navigationBtn[0].onClick.AddListener(NavigateLeft);
    navigationBtn[1].onClick.AddListener(NavigateRight);

    changeModeBtn[0].onClick.AddListener(delegate { ChangeMode(0); });
    changeModeBtn[1].onClick.AddListener(delegate { ChangeMode(1); });

    itemSlotBtn[0].onClick.AddListener(delegate { UpdateInfo(0); });
    itemSlotBtn[1].onClick.AddListener(delegate { UpdateInfo(1); });
    itemSlotBtn[2].onClick.AddListener(delegate { UpdateInfo(2); });
    itemSlotBtn[3].onClick.AddListener(delegate { UpdateInfo(3); });

    buyBtn[0].onClick.AddListener(delegate { BuyEquip(0); });
    buyBtn[1].onClick.AddListener(delegate { BuyEquip(1); });
    buyBtn[2].onClick.AddListener(delegate { BuyEquip(2); });
    buyBtn[3].onClick.AddListener(delegate { BuyEquip(3); });
  }

  private void ChangeMode(int mode)
  {
    if (mode == 0)
    {
      storeMode = Mode.Buff;
      ChangeColorMode(0);
    }
    else if (mode == 1)
    {
      storeMode = Mode.Skill;
      ChangeColorMode(1);
    }

    UpdateSlot();
    UpdateInfo(0);
    currentSlot = 0;
  }

  private void ChangeColorMode(int index)
  {
    if (index == 0)
    {
      changeModeBtn[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
      changeModeBtn[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
    else if (index == 1)
    {
      changeModeBtn[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
      changeModeBtn[0].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
  }

  private void Exit()
  {
    playerStatus.canattack = true;
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

  private void OnEnable()
  {
    storeMode = Mode.Buff;
    currentSlot = 0;
    UpdateSlot();
    UpdateInfo(0);
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

    if ((storeMode == Mode.Buff && currentSlot + 4 < buffList.Count) || (storeMode == Mode.Skill && currentSlot + 4 <= equipmentStock.skillList.Count))
    {
      navigationBtn[1].gameObject.SetActive(true);
    }
    else
    {
      navigationBtn[1].gameObject.SetActive(false);
    }

    for (int i = 0; i < 4; i++)
    {
      if ((storeMode == Mode.Buff && i + currentSlot >= buffList.Count) || (storeMode == Mode.Skill && i + currentSlot >= equipmentStock.skillList.Count))
      {
        sprite[i].sprite = alpha;
        price[i].text = "";
      }
      else
      {
        if (storeMode == Mode.Buff)
        {
          sprite[i].sprite = buffList[currentSlot + i].image;
          price[i].text = buffList[currentSlot + i].price.ToString() + "$";
        }
        else if (storeMode == Mode.Skill)
        {
          sprite[i].sprite = equipmentStock.skillList[currentSlot + i].image;
          price[i].text = equipmentStock.skillList[currentSlot + i].price.ToString() + "$";
        }
      }
    }

    UpdateInfo(lastActiveEquip);
  }

  private void UpdateInfo(int index)
  {
    // Update color
    if ((storeMode == Mode.Buff && index + currentSlot >= buffList.Count) || (storeMode == Mode.Skill && index + currentSlot >= equipmentStock.skillList.Count))
    {
      return;
    }

    Color greenColor;
    ColorUtility.TryParseHtmlString("#69E5B2", out greenColor);
    if (lastActiveEquip != -1)
    {
      itemSlotBtn[lastActiveEquip].gameObject.GetComponent<Image>().color = greenColor;
    }

    itemSlotBtn[index].gameObject.GetComponent<Image>().color = Color.red;

    lastActiveEquip = index;



    if (storeMode == Mode.Buff)
    {
      BuffSO item = buffList[currentSlot + index];
      nameEquip.text = item.name;

      itemDescription.text = item.description;
    }
    else if (storeMode == Mode.Skill)
    {
      MonsterSkillSO skill = equipmentStock.skillList[currentSlot + index];
      nameEquip.text = skill.name;
      itemDescription.text = skill.description;
    }
  }

  private void BuyEquip(int index)
  {
    if ((storeMode == Mode.Buff && currentSlot + index > buffList.Count) ||
      (storeMode == Mode.Skill && currentSlot + index > equipmentStock.skillList.Count))
    {
      return;
    }

    if ((storeMode == Mode.Buff && buffList[currentSlot + index].price > playerStatus.GetPoint()) ||
      (storeMode == Mode.Skill && equipmentStock.skillList[currentSlot + index].price > playerStatus.GetPoint()))
    {
      return;
    }

    if (storeMode == Mode.Buff)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() - buffList[currentSlot + index].price);
      playerStatus.buffList.Add(buffList[currentSlot + index]);
      buffList[currentSlot + index].Activate(playerStatus);
      pointText.text = playerStatus.GetPoint().ToString();
      UpdateSlot();
    }

    else if (storeMode == Mode.Skill)
    {
      playerStatus.SetPoint(playerStatus.GetPoint() - equipmentStock.skillList[currentSlot + index].price);
      equipmentStock.skillList[currentSlot + index].Activate(playerStatus);
      equipmentStock.skillList.RemoveAt(currentSlot + index);

      pointText.text = playerStatus.GetPoint().ToString();
      UpdateSlot();
    }
  }
}
