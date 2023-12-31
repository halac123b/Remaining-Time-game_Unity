using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using TMPro;
// using UnityEditor;

public class PlayerAnimator : AnimatorController
{
  [SerializeField] private Animator cover_animator;
  [SerializeField] private Animator weapon_animator;


  private PlayerEquip playerEquip;
  private PlayerColision playerColision;
  public Weapon weapon;

  [SerializeField] private SpriteRenderer weaponcarry;
  [SerializeField] private SpriteRenderer cover_sprite;
  [SerializeField] private SpriteRenderer weapon_sprite;
  [SerializeField] private SpriteRenderer sprite;
  [SerializeField] private GameObject playerPos;

  [SerializeField] private TextMeshPro playername;
  [SerializeField] private Transform TimeBar;
 
  public Transform AimBar;

  private NetworkVariable<bool> flipX = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<bool> weaponCarry = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  private NetworkVariable<Vector2> mouse = new NetworkVariable<Vector2>(new Vector2(0, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private NetworkVariable<int> weaponSorting = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  protected override void Awake()
  {

    base.Awake();
    playerColision = GetComponentInParent<PlayerColision>();
    playerEquip = FindObjectOfType<PlayerEquip>();

    // NetWork Variable
    flipX.OnValueChanged += OnFlipXChanged;
    weaponSorting.OnValueChanged += OnWeaponSortChanged;
    weaponCarry.OnValueChanged += OnWeaponCarryChanged;
    playerData.OnValueChanged += OnPlayerDataChanged;

    // Handle Event Custom
    playerEquip.OnChangeEquip += OnChangeEquipped;

    playerInput.playerInputActions.Player.Attack.started += TriggerAttackStarted;
  }

  override public void OnDestroy()
  {
    if (playerEquip)playerEquip.OnChangeEquip -= OnChangeEquipped;

    if (playerInput) playerInput.playerInputActions.Player.Attack.started -= TriggerAttackStarted;

    if (playerStatus) playerStatus.OnDeadTrigger -= OnDeadAnimation;

    base.OnDestroy();
  }

  public override void OnNetworkSpawn()
  {
    if (IsOwner)
    {
      playerStatus.OnDeadTrigger += OnDeadAnimation;
    }

  }

  protected override void Start()
  {
    base.Start();
    if (!IsOwner) return;
    AimBar.GetComponentInChildren<Slider>().value = 0;
    AimBar.gameObject.SetActive(false);
    
    PlayerData data = new PlayerData
    {
      Id = playerData.Value.Id,
      color = playerData.Value.color,
      playerName = playerData.Value.playerName,
      playerWeapon = (playerEquip.GetCurrentEquip() == null) ? -1 : playerEquip.GetCurrentEquip().GetTypeWeapon(),
    };
    playerData.Value = data;

    if (IsOwner)
    {
      sprite.material.color = cover_sprite.material.color = playerData.Value.color;
    }
  }



  private void FixedUpdate()
  {
    playername.text = playerData.Value.playerName;
    if (playerData.Value.playerWeapon == 4 && AimBar.GetComponentInChildren<Slider>().value > 0)
    {
      AimBar.gameObject.SetActive(true);
      AimBar.GetComponentInChildren<Slider>().value -= 0.002f;
    }
    else if (playerData.Value.playerWeapon == 4 && AimBar.GetComponentInChildren<Slider>().value <= 0)
    {
      AimBar.gameObject.SetActive(false);
    }
    CountDownTimer countDownTimer = TimeBar.GetComponentInChildren<CountDownTimer>();

    TimeBar.GetComponentInChildren<Slider>().value =  (float)countDownTimer.Time.Value/ (float)countDownTimer.TimeMax.Value ;
    countDownTimer.textContent.text = String.Format($"{countDownTimer.Time.Value / 60:D2}:{countDownTimer.Time.Value % 60:D2}");
  }
  protected override void Update()
  {
    if (!IsOwner) return;
    // Debug.LogWarning(NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetCurrentRtt(NetworkManager.Singleton.LocalClientId));

    animator.SetInteger(TYPE_MOVE, playerMovement.GetTypeMove());

    // weaponcarry.sprite = playerEquip.GetCurrentEquip().GetSprite();

    animator.SetFloat(SPEED, playerMovement.MoveVector().magnitude);
    animator.SetInteger(TYPE_MOVE, playerMovement.GetTypeMove());

    float x = playerMovement.MoveVector().x;
    float y = playerMovement.MoveVector().y;

    Set_VERTICAL_HORIZONTAL(x, y);
    animator.SetBool(IS_PROCESSING, playerColision.IsInProcessing());
  }

  /////////////////////Support////////////////////////////////
  public void UpdataMousePos()
  {
    if (!IsOwner) return;
    // Update mouse position
    Vector2 mouse_pos = Input.mousePosition;
    mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    mouse.Value = mouse_pos;
  }
  public Vector2 GetMousePos()
  {
    return mouse.Value;
  }
  public void SetWeaponCarry(bool active)
  {
    if (IsOwner)
      weaponCarry.Value = active;
  }
  public override void Set_VERTICAL_HORIZONTAL(float x, float y)
  {
    base.Set_VERTICAL_HORIZONTAL(x, y);
    if (!IsOwner) return;
    Set_VERTICAL_HORIZONTAL(cover_animator, x, y);
    Set_VERTICAL_HORIZONTAL(weapon_animator, x, y);
  }

  [ClientRpc]
  public void AstronautHurtClientRpc(int dame, Vector2 pos, int nockBack)
  {
    if (!IsOwner) return;

    if (playerStatus.GetTimeLeft() > 0)
    {

      animator.SetFloat("dame", dame);
      animator.SetTrigger(HURT);
      GetComponentInParent<Rigidbody2D>().AddForce(pos.normalized * nockBack, ForceMode2D.Impulse);
      playerStatus.SetTimeLeft(playerStatus.GetTimeLeft() - dame);

    }
  }
  // public void DestroyObj()
  // {
  //   //Destroy(GetComponentInParent<PlayerMovement>().gameObject);
  // }
  public void HurtFloating(string text)
  {
    GameObject floatingtext = Instantiate(FloatingText, playerMovement.transform.position, Quaternion.identity, playerMovement.transform);
    floatingtext.GetComponent<TextMeshPro>().text = text;
    floatingtext.GetComponent<TextMeshPro>().color = Color.red;
  }
  public override void Set_VERTICAL_HORIZONTAL(Animator anim, float x, float y)
  {

    if (x <= -0.01f)
    {
      flipX.Value = false;
    }
    else if (x >= 0.01f)
    {
      flipX.Value = true;
    }

    anim.SetInteger(TYPE_ATTACK, playerData.Value.playerWeapon);

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
  public void UpdatePlayerColor(Color color)
  {
    sprite.material.color = color;
    cover_sprite.material.color = color;
  }
  /////////////////////////////Handle Event////////////////////////////// 
  private void OnChangeEquipped(object sender, EventArgs e)
  {
    if (!IsOwner) return;
    if (AimBar && AimBar.GetComponentInChildren<Slider>())
    {
      AimBar.GetComponentInChildren<Slider>().value = 0;
      AimBar.gameObject.SetActive(false);
    }

    PlayerData data = new PlayerData
    {
      Id = playerData.Value.Id,
      color = playerData.Value.color,
      playerName = playerData.Value.playerName,
      playerWeapon = playerEquip.GetCurrentEquip().GetTypeWeapon(),
    };

    if (animator) animator.ResetTrigger(ATTACK);
    if (weapon_animator) weapon_animator.ResetTrigger(ATTACK);
    if (cover_animator) cover_animator.ResetTrigger(ATTACK);

    playerData.Value = data;

  }
  private void OnFlipXChanged(bool oldValue, bool newValue)
  {
    weaponcarry.flipX = cover_sprite.flipX = sprite.flipX = newValue;
    if (newValue) weapon_sprite.transform.localScale = new Vector3(-1, 1, 1);
    else weapon_sprite.transform.localScale = new Vector3(1, 1, 1);
  }
  private void OnDeadAnimation(object sender, EventArgs e)
  {
    if (animator == null)
    {
      return;
    }
    animator.SetTrigger("isDeath");

    if (GetComponentInParent<PlayerColision>().IsInProcessing())
    {
      FindObjectOfType<OxyStatus>().SetProcessServerRpc(false, -playerStatus.processSpeed);
    }
  }

  [ServerRpc(RequireOwnership = false)]
  private void DestroyPlayerServerRpc()
  {
    DestroyClientRpc();
  }

  [ClientRpc]
  private void DestroyClientRpc()
  {
    Destroy(playerMovement.gameObject);
  }

  private void OnWeaponCarryChanged(bool previousValue, bool newValue)
  {
    weaponcarry.gameObject.SetActive(newValue);
  }

  private void OnWeaponSortChanged(int previousValue, int newValue)
  {
    weaponcarry.sortingOrder = newValue;
  }

  private void OnPlayerDataChanged(PlayerData previousValue, PlayerData newValue)
  {
    if (sprite && cover_sprite)
      sprite.material.color = cover_sprite.material.color = playerData.Value.color;
    if (playerEquip.GetEquip(playerData.Value.playerWeapon) != null)
    {
      // Debug.LogError("playerData.Value.playerWeapon: "+playerData.Value.playerWeapon);
      if (weaponcarry) weaponcarry.sprite = playerEquip.GetEquip(playerData.Value.playerWeapon).GetSprite();
    }
  }
  protected void TriggerAttackStarted(InputAction.CallbackContext context)
  {
    if (!IsOwner || animator == null || !playerStatus.canattack || !playerStatus.canMove) return;
    animator.SetTrigger(ATTACK);

    if (weapon_animator == null || cover_animator == null) return;
    weapon_animator.SetTrigger(ATTACK);
    cover_animator.SetTrigger(ATTACK);
  }
  protected override void TriggerAttackCanceled(InputAction.CallbackContext context)
  {
    base.TriggerAttackCanceled(context);
    if (!IsOwner || weapon_animator == null || cover_animator == null) return;
    weapon_animator.SetTrigger(ATTACK_CANCEL);
    cover_animator.SetTrigger(ATTACK_CANCEL);
  }
}