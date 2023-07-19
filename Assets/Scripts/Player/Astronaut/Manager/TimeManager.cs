using UnityEngine;

public class TimeManager : MonoBehaviour
{
  [SerializeField] private int startTimer = 30;

  // public int GetRandomIndex()
  // {
  //   System.Random rand = new System.Random();
  //   int index = indexArray[rand.Next(0, indexArray.Count)];
  //   indexArray.RemoveAt(index);
  //   return index;
  // }

  public void SetTimer(int amount)
  {
    startTimer = amount;
  }

  public int GetTimer()
  {
    return startTimer;
  }
}
