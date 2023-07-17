using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerAnim : AnimatorController
{
  [SerializeField] private Animator cover_animator;
  [SerializeField] private Animator weapon_animator;


  private PlayerEquip playerEquip;
  private PlayerColision playerColision;

  [SerializeField] private SpriteRenderer weaponcarry;
  [SerializeField] private SpriteRenderer cover_sprite;
  [SerializeField] private SpriteRenderer weapon_sprite;

  [SerializeField] private SpriteRenderer sprite;

  protected NetworkVariable<bool> flipX = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<bool> weaponCarry = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<Vector2> mouse = new NetworkVariable<Vector2>(new Vector2(0,0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private NetworkVariable<int> weaponSorting = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  protected NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>(
    new PlayerData
    {
      Id = "",
      color = Color.red,
      playerName = ""
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  protected override void Awake()
  {
    base.Awake();
    playerColision = GetComponentInParent<PlayerColision>();
    playerEquip = FindObjectOfType<PlayerEquip>();

    // NetWork Variable
    flipX.OnValueChanged += OnFlipXChanged;
    weaponSorting.OnValueChanged += OnWeaponSortChanged;
    weaponCarry.OnValueChanged += OnWeaponCarryChanged;
  }

  private void OnWeaponCarryChanged(bool previousValue, bool newValue)
  {
    weaponcarry.gameObject.SetActive(newValue);
  }

  private void OnWeaponSortChanged(int previousValue, int newValue)
  {
    weaponcarry.sortingOrder = newValue;
  }

  protected override void TriggerAttackStarted(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    base.TriggerAttackStarted(context);
    weapon_animator.SetTrigger(ATTACK);
    cover_animator.SetTrigger(ATTACK);
  }
  public void UpdataMousePos(){
    // Update mouse position
    Vector2 mouse_pos = Input.mousePosition;
    mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    mouse.Value = mouse_pos;
  }
  protected override void TriggerAttackCanceled(InputAction.CallbackContext context)
  {
    if (!IsOwner) return;

    

    base.TriggerAttackCanceled(context);
    weapon_animator.SetTrigger(ATTACK_CANCEL);
    cover_animator.SetTrigger(ATTACK_CANCEL);
  }

  public Vector2 GetMousePos(){
    return mouse.Value;
  }

  private void OnPlayerDataChanged(PlayerData previousValue, PlayerData newValue)
  {
    sprite.material.color = cover_sprite.material.color = playerData.Value.color;

  }

  public void SetWeaponCarry(bool active)
  {
    if (IsOwner)
      weaponCarry.Value = active;
  }

  private void Start()
  {
    if (IsOwner) playerData.Value = playerStatus.GetPlayerData();
  }

  protected override void Update()
  {
    sprite.material.color = cover_sprite.material.color = playerData.Value.color;
    if (!IsOwner) return;

    weaponcarry.sprite = playerEquip.GetCurrentEquip().GetSprite();

    animator.SetFloat(SPEED, playerMovement.MoveVector().magnitude);
    animator.SetInteger(TYPE_MOVE, playerMovement.GetTypeMove());

    float x = playerMovement.MoveVector().x;
    float y = playerMovement.MoveVector().y;

    Set_VERTICAL_HORIZONTAL(x,y);
    animator.SetBool(IS_PROCESSING, playerColision.IsInProcessing());

    


  }

  public override void Set_VERTICAL_HORIZONTAL(float x, float y){
    if(!IsOwner) return;
    Set_VERTICAL_HORIZONTAL(animator, x, y);
    Set_VERTICAL_HORIZONTAL(cover_animator, x, y);
    Set_VERTICAL_HORIZONTAL(weapon_animator, x, y);
  }
  private void Set_VERTICAL_HORIZONTAL(Animator anim, float x, float y){

     if (x <= -0.01f)
    {
      flipX.Value = false;
    }
    else if (x >= 0.01f)
    {
      flipX.Value = true;
    }

    anim.SetInteger(TYPE_ATTACK, playerEquip.GetTypeWeapon());

    if (y > 0.01f)
    {
      weaponSorting.Value = 1;
      anim.SetFloat(VERTICAL, 1f);
    }
    else if (y < -0.01f)
    {
      weaponSorting.Value = -1;
      anim.SetFloat(VERTICAL, -1f);
    }

    if (x > 0.01f)
    {
      weaponSorting.Value = -1;
      anim.SetFloat(VERTICAL, 0f);
      anim.SetFloat(HORIZONTAL, 1f);
    }
    else if (x < -0.01f)
    {
      weaponSorting.Value = -1;
      anim.SetFloat(VERTICAL, 0f);
      anim.SetFloat(HORIZONTAL, -1f);
    }
  }

  private void OnFlipXChanged(bool oldValue, bool newValue)
  {
    weaponcarry.flipX = weapon_sprite.flipX = cover_sprite.flipX = sprite.flipX = newValue;
  }

  public void UpdatePlayerColor(Color color)
  {
    sprite.material.color = color;
    cover_sprite.material.color = color;
  }
}