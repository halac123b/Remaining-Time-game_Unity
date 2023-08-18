using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using TMPro;

public class MonsterAnimator : AnimatorController
{
  // Start is called before the first frame update
  [SerializeField] public Transform AimBar;
  [SerializeField] public Transform HPbar;
  [SerializeField] public MonsterSword monsterSword;
  [SerializeField] private TextMeshPro playername;
 
  public int NumMonsterReal;

  private NetworkVariable<Vector2> mouse = new NetworkVariable<Vector2>(new Vector2(0, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  public NetworkVariable<ulong> index = new NetworkVariable<ulong>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  public NetworkVariable<float> HP = new NetworkVariable<float>(300f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private PlayerEquip playerEquip;

  public const float TIME = 1f;
  public float time = 0;

  protected override void Awake()
  {
    base.Awake();
    NumMonsterReal = playerStatus.NumMonster;
    HP.Value = 300f;
    playerInput.playerInputActions.Player.Attack.started += TriggerAttack01Started;
    playerInput.playerInputActions.Player.Attack02.started += TriggerAttack02Started;
    playerInput.playerInputActions.Player.Attack03.started += TriggerAttack03Started;

    playerInput.playerInputActions.Player.E_Btn.started += TriggerSummon;

    playerEquip = FindAnyObjectByType<PlayerEquip>();
  }

  public override void OnDestroy()
  {
    playerInput.playerInputActions.Player.Attack.started -= TriggerAttack01Started;
    playerInput.playerInputActions.Player.Attack02.started -= TriggerAttack02Started;
    playerInput.playerInputActions.Player.Attack03.started -= TriggerAttack03Started;

    playerInput.playerInputActions.Player.E_Btn.started -= TriggerSummon;

    base.OnDestroy();
  }

  public bool Is_Server()
  {
    return IsServer;
  }
  protected override void Start()
  {
    base.Start();
    if (!IsOwner) return;
  }

  private void FixedUpdate()
  {
    playername.text = playerData.Value.playerName;

  }
  protected override void Update()
  {
    base.Update();
    garen_cd -= Time.deltaTime;
    ezreal_cd -= Time.deltaTime;
    time += Time.deltaTime;
    // Debug.LogError(GetComponentInChildren<Slider>()+" "+ HP.Value +" "+ playerStatus.GetMax_HP());
    HPbar.GetComponentInChildren<Slider>().value = HP.Value / playerStatus.GetMax_HP();
    if (!IsOwner) return;
    // if (!playerStatus.canMove) Debug.LogError("Cannot Move"); 
  }

  /////////////////////Support////////////////////////////////
  public Vector2 GetMousePos()
  {
    return mouse.Value;
  }
  public void ADDhp(float hp)
  {
    if (IsOwner) HP.Value += hp;
  }
  public void SetMove(bool canMove)
  {
    if (IsOwner)
    {
      playerStatus.canMove = canMove;
    }
  }
  public void UpdataMousePos()
  {
    if (!IsOwner) return;
    // Update mouse position
    Vector2 mouse_pos = Input.mousePosition;
    mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    mouse.Value = mouse_pos;
  }
  [ClientRpc]
  public void GetHurtClientRpc(int dame, Vector2 pos, int nockBack, ulong id)
  {
    if (!IsOwner) return;
    index.Value = id;
    if (HP.Value > 0 && time >= TIME)
    {
      time = 0;
      animator.SetTrigger(HURT);
      GetComponentInParent<Rigidbody2D>().AddForce(pos.normalized * nockBack, ForceMode2D.Impulse);
      HP.Value -= dame;
    }
    if (HP.Value <= 0)
    {
      animator.SetTrigger(DEATH);
      Destroy(animator.GetComponent<CapsuleCollider2D>());
    }
  }
  public void CreateTrigger()
  {
    monsterSword.CreateTrigger();
    // monsterSword.CreateTrigger(animator.GetInteger(TYPE_ATTACK));
  }
  public void DelTrigger(){
    monsterSword.DelTrigger();
  }
  public int GetDmg()
  {
    return playerStatus.monsterAttack;
  }
  public void ShowFloatText(string text)
  {

    foreach (var o in FindObjectsByType<PlayerAnimator>(FindObjectsSortMode.InstanceID))
    {
      if (o.GetPlayerData().Id == index.Value)
      {
        GameObject floatingtext = Instantiate(o.FloatingText, o.playerMovement.transform.position, Quaternion.identity, o.playerMovement.transform);
        floatingtext.GetComponent<TextMeshPro>().text = text;
        floatingtext.GetComponent<TextMeshPro>().color = Color.blue;
        o.weapon.increaseTime(20);
      }
    }
  }
  /////////////////////////////Handle Event////////////////////////////// 


  // private void TriggerSummonHunter(InputAction.CallbackContext context)
  // {
  //   if (IsOwner && playerStatus.canMove && NumMonsterReal > 0)
  //     animator.SetTrigger("summonHunter");
  // }
  private void TriggerSummon(InputAction.CallbackContext context)
  {

    if (IsOwner && playerEquip.canTriggerSkill && playerEquip.GetCurrentMonster() != null && NumMonsterReal > 0)
    {
      switch (playerEquip.GetCurrentMonster().TypeWeapon)
      {
        case 0:
          animator.SetTrigger("summonHunter");
          break;
        case 1:
          animator.SetTrigger("summonGrunt");
          break;
      }
    }
  }
  private void TriggerAttack01Started(InputAction.CallbackContext context)
  {
    if (!IsOwner || animator == null || !playerStatus.canMove || !playerStatus.canattack ) return;
    animator.SetInteger(TYPE_ATTACK, 1);
    animator.SetTrigger(ATTACK);
  }

  public float garen_cd = 5;
  public float ezreal_cd = 3;
  private void TriggerAttack02Started(InputAction.CallbackContext context)
  {
    if (!IsOwner || !playerStatus.canMove || !playerStatus.canattack || !playerStatus.garenEnable || garen_cd > 0) return;
    animator.SetInteger(TYPE_ATTACK, 2);
    animator.SetTrigger(ATTACK);
    garen_cd = 5;
  }
  private void TriggerAttack03Started(InputAction.CallbackContext context)
  {
    if (!IsOwner || !playerStatus.canMove || !playerStatus.canattack || !playerStatus.ezrealEnable || ezreal_cd > 0) return;
    animator.SetInteger(TYPE_ATTACK, 3);
    animator.SetTrigger(ATTACK);
    ezreal_cd = 3;
  }

  // Update is called once per frame
}
