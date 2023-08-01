using UnityEngine;

[CreateAssetMenu(menuName = "Ezreal Skill", fileName = "New Skill")]
public class EzrealSkillSO : MonsterSkillSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.ezrealEnable = true;
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.ezrealEnable = false;
  }
}
