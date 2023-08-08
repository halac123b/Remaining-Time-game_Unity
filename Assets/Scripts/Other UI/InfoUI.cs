using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoUI : MonoBehaviour
{
  private PlayerStatus playerStatus;

  [SerializeField] private TextMeshProUGUI speed;
  [SerializeField] private TextMeshProUGUI protect;
  [SerializeField] private TextMeshProUGUI process;
  [SerializeField] private TextMeshProUGUI damage;
  [SerializeField] private TextMeshProUGUI reborn;
  [SerializeField] private TextMeshProUGUI health;

  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
  }

  private void OnEnable()
  {
    speed.text = playerStatus.moveSpeed.ToString();
    protect.text = playerStatus.protection.ToString();
    process.text = playerStatus.processSpeed.ToString();
    damage.text = playerStatus.monsterAttack.ToString();
    reborn.text = playerStatus.monsterRebornTime.ToString();
    health.text = playerStatus.monsterHealth.ToString();
  }
}
