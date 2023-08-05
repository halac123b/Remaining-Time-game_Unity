using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class MonsterAnimator : AnimatorController
{
  public float MAX_HP = 300f;
  // Start is called before the first frame update
  [SerializeField] public Transform AimBar;
  [SerializeField] public Transform HPbar;
 
    protected const string HURT = "hurt";
    protected const string DEATH = "death";

   private NetworkVariable<Vector2> mouse = new NetworkVariable<Vector2>(new Vector2(0, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  public NetworkVariable<ulong> index = new NetworkVariable<ulong>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  public NetworkVariable<float> HP =  new NetworkVariable<float>(300f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  
  private PlayerEquip playerEquip;

  public const float TIME  = 1f;
  public float time = 0;

  
  protected override void Awake()
  {
    base.Awake();
    HP.Value = 300f;
    playerInput.playerInputActions.Player.Attack.started += TriggerAttack01Started;
    playerInput.playerInputActions.Player.Attack02.started += TriggerAttack02Started;
    playerInput.playerInputActions.Player.Attack03.started += TriggerAttack03Started;

    playerInput.playerInputActions.Player.E_Btn.started += TriggerSummon;  

    playerEquip = FindAnyObjectByType<PlayerEquip>();
  }
  public bool Is_Server()
  {
    return IsServer;
  }
  protected override void Start()
  {
    base.Start();
    if (!IsOwner) return;
    playerStatus.Renew();

  }

  protected override void Update()
  {
    base.Update();
    time += Time.deltaTime;
    HPbar.GetComponentInChildren<Slider>().value = HP.Value / MAX_HP;
    if (!IsOwner) return;
    // if (!playerStatus.canMove) Debug.LogError("Cannot Move"); 
  }

  /////////////////////Support////////////////////////////////
  public Vector2 GetMousePos()
  {
    return mouse.Value;
  }
  public void ADDhp (float hp){
    if(IsOwner) HP.Value+=hp;
  }
  public void SetMove (bool canMove){
     if (IsOwner){
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
    public void GetHurtClientRpc(int dame,Vector2 pos,int nockBack,ulong id){
        if (!IsOwner ) return;
        index.Value = id;
        if (HP.Value > 0 && time >= TIME) {
            time = 0;
            animator.SetTrigger(HURT);
            GetComponentInParent<Rigidbody2D>().AddForce(pos.normalized*nockBack,ForceMode2D.Impulse);
            HP.Value -= dame;
        }    
        if (HP.Value <=0  )
        {
            animator.SetTrigger(DEATH);
            Destroy(animator.GetComponent<CapsuleCollider2D>());
        }
        

    }
    public void ShowFloatText(string text){

            foreach (var o in FindObjectsByType<PlayerAnimator>(FindObjectsSortMode.InstanceID)){
                if (o.GetPlayerData().Id == index.Value){
                    GameObject floatingtext = Instantiate(o.FloatingText,o.playerMovement.transform.position, Quaternion.identity,o.playerMovement.transform);
                    floatingtext.GetComponent<TextMesh>().text = text;
                    o.weapon.increaseTime(20);
                }
            }
    }
  /////////////////////////////Handle Event////////////////////////////// 

  private void TriggerSummonHunter(InputAction.CallbackContext context)
  {
    if (IsOwner && playerStatus.canMove)
      animator.SetTrigger("summonHunter");
  }
  private void TriggerSummon(InputAction.CallbackContext context)
  {
  
    if (IsOwner && playerEquip.canTriggerSkill && playerEquip.GetCurrentMonster() != null)
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
    if (!IsOwner || animator == null || !playerStatus.canMove) return;
    animator.SetInteger(TYPE_ATTACK, 1);
    animator.SetTrigger(ATTACK);
  }

  private void TriggerAttack02Started(InputAction.CallbackContext context)
  {
    if (!IsOwner || !playerStatus.canMove) return;
    animator.SetInteger(TYPE_ATTACK, 2);
    animator.SetTrigger(ATTACK);
  }
  private void TriggerAttack03Started(InputAction.CallbackContext context)
  {
    if (!IsOwner || !playerStatus.canMove) return;
    animator.SetInteger(TYPE_ATTACK, 3);
    animator.SetTrigger(ATTACK);
  }

  // Update is called once per frame
}
