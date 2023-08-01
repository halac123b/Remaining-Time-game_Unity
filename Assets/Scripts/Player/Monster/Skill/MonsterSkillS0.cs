using UnityEngine;

public class MonsterSkillSO : ScriptableObject
{
  public string buffName = "Enter name of Skill";
  public Sprite image;
  public int price = 30;
  public string description = "Describe it";

  virtual public void Activate(PlayerStatus playerStatus)
  {
  }

  virtual public void DeActivate(PlayerStatus playerStatus)
  {
  }
}
