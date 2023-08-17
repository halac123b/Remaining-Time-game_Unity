using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
  private PlayerStatus playerStatus;

  [SerializeField] private Image ezreal;
  [SerializeField] private Image garen;

  [SerializeField] private Sprite alpha;
  [SerializeField] private Sprite ezrealSprite;
  [SerializeField] private Sprite garenSprite;

  MonsterAnimator monsterAnimator;

  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
  }

  private void Update()
  {
    if (monsterAnimator == null)
    {
      monsterAnimator = FindObjectOfType<MonsterAnimator>();
    }
    if (playerStatus.ezrealEnable == true)
    {
      ezreal.sprite = ezrealSprite;

      if (monsterAnimator != null)
      {
        ezreal.fillAmount = Mathf.Clamp((5 - monsterAnimator.ezreal_cd) / 5, 0, 1);
      }
    }
    else
    {
      ezreal.sprite = alpha;
    }

    if (playerStatus.garenEnable == true)
    {
      garen.sprite = garenSprite;

      if (monsterAnimator != null)
      {
        garen.fillAmount = Mathf.Clamp((3 - monsterAnimator.garen_cd) / 3, 0, 1);
      }
    }
    else
    {
      garen.sprite = alpha;
    }
  }
}
