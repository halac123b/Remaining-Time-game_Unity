using UnityEngine;
using System;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerAnimator : NetworkBehaviour
{
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";
  private const string IS_PROCESSING = "isprocessing";
  private const string SPEED = "speed";
  private const string TYPE_MOVE = "typemove";
  private const string TYPE_ATTACK = "typeattack";
  private const string ATTACK = "attack";
  private const string ATTACK_CANCEL = "attackcancel";


  [SerializeField] private Animator animator;
  [SerializeField] private Animator cover_animator;
  [SerializeField] private Animator weapon_animator;

  private PlayerStatus playerStatus;
  private PlayerEquip playerEquip;

  private PlayerInput playerInput;
  private PlayerMovement playerMovement;
  private PlayerColision playerColision;

  [SerializeField] private SpriteRenderer weaponcarry;
  [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private SpriteRenderer cover_sprite;
  [SerializeField] private SpriteRenderer weapon_sprite;

  private NetworkVariable<bool> weaponCarry = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<int> weaponSorting = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<bool> flipX = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>(
    new PlayerData
    {
      Id = "",
      color = Color.red,
      playerName = ""
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private void Awake()
  {

    playerInput = GetComponentInParent<PlayerInput>();
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerColision = GetComponentInParent<PlayerColision>();
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerEquip = FindObjectOfType<PlayerEquip>();

    playerStatus.OnDeadTrigger += OnDeadAnimation;

    // NetWork Variable
    flipX.OnValueChanged += OnFlipXChanged;
    playerData.OnValueChanged += OnPlayerDataChanged;
    weaponSorting.OnValueChanged += OnWeaponSortChanged;
    weaponCarry.OnValueChanged += OnWeaponCarryChanged;

    //PlayerInput
    playerInput.playerInputActions.Player.Attack.performed += TriggerAttackPerformed;
    playerInput.playerInputActions.Player.Attack.canceled += TriggerAttackCanceled;

    


  }

    private void TriggerAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
      weapon_animator.SetTrigger(ATTACK_CANCEL);
      cover_animator.SetTrigger(ATTACK_CANCEL);
      animator.SetTrigger(ATTACK_CANCEL);
    }

    private void OnWeaponCarryChanged(bool previousValue, bool newValue)
  {
    weaponcarry.gameObject.SetActive(newValue);
  }

  private void OnWeaponSortChanged(int previousValue, int newValue)
  {
    weaponcarry.sortingOrder = newValue;
  }

  private void TriggerAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (!IsOwner) return;
    weapon_animator.SetTrigger(ATTACK);
    cover_animator.SetTrigger(ATTACK);
    animator.SetTrigger(ATTACK);
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

  private void Update()
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
  public void Set_VERTICAL_HORIZONTAL(float x, float y){
    if(!IsOwner) return;
    Set_VERTICAL_HORIZONTAL(animator, x, y);
    Set_VERTICAL_HORIZONTAL(cover_animator, x, y);
    Set_VERTICAL_HORIZONTAL(weapon_animator, x, y);
  }
  private void Set_VERTICAL_HORIZONTAL(Animator anim, float x, float y)
  {
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

  private void OnDeadAnimation(object sender, EventArgs e)
  {
    animator.SetTrigger("isDeath");
    playerMovement.enabled = false;
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
