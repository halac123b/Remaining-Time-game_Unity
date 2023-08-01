using UnityEngine;

public class BuffSO : ScriptableObject
{
  public string buffName = "Enter name of buff";
  public Sprite image;
  public int price = 30;
  public float value = 1;
  public string description = "Describe it";

  virtual public void Activate(PlayerStatus playerStatus)
  {
  }

  virtual public void DeActivate(PlayerStatus playerStatus)
  {
  }
}
