using UnityEngine;

[CreateAssetMenu(menuName = "Garen Skill", fileName = "New Skill")]
public class GarenSkillSO : MonsterSkillSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.garenEnable = true;
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.garenEnable = false;
  }
}
