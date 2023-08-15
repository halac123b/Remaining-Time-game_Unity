using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkeletonGruntAnimation : NetworkBehaviour
{
  protected const string HORIZONTAL = "Horizontal";
  protected const string VERTICAL = "Vertical";
  protected const string SPEED = "speed";
  protected const string ATTACK = "attack";
  protected const string ATTACK_CANCEL = "attackcancel";
  protected const string HURT = "hurt";
  protected const string DEATH = "death";


  // Start is called before the first frame update
  [SerializeField] SkeletonMovement skeletonMovement;
  [SerializeField] Animator animator;
  [SerializeField] public Transform AimBar;
  [SerializeField] public SubMonsterSword subMonsterSword;

  private float countDownt = 0;
  public NetworkVariable<ulong> index = new NetworkVariable<ulong>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  // Update is called once per frame

  public const float TIME = 1f;
  public float time = 0;

  void Update()
  {
    time += Time.deltaTime;
    AimBar.GetComponentInChildren<Slider>().value = skeletonMovement.HP.Value / 100f;
    if (skeletonMovement.HP.Value <= 0)
    {
      return;
    }
    countDownt += Time.deltaTime;
    Vector3 direction = skeletonMovement.Getdirection();
    float x = direction.x;
    float y = direction.y;

    if (y > 0.01f)
    {
      animator.SetFloat(VERTICAL, 1f);
    }
    else if (y < -0.01f)
    {
      animator.SetFloat(VERTICAL, -1f);
    }

    if (x > 0.3f)
    {
      animator.SetFloat(VERTICAL, 0f);
      animator.SetFloat(HORIZONTAL, 1f);
    }
    else if (x < -0.3f)
    {
      animator.SetFloat(VERTICAL, 0f);
      animator.SetFloat(HORIZONTAL, -1f);
    }

    animator.SetFloat(SPEED, direction.magnitude);
    if (!skeletonMovement.canmove) animator.SetFloat(SPEED, 0);

    if (InRangeAttack() && countDownt >= 2)
    {
      animator.SetTrigger(ATTACK);
      countDownt = 0;
    }
    if (InRangeAttack()) skeletonMovement.canmove = false;
    else skeletonMovement.canmove = true;
  }

  /////////////////////Support//////////////////////////

  private bool InRangeAttack()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + 1f), 0.6f);

    // Lọc ra vật thể gần nhất có layer là "Player"
    Transform nearestPlayer = null;
    float nearestDistance = Mathf.Infinity;
    foreach (Collider2D collider in colliders)
    {
      if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerPos"))
      {
        float distance = Vector2.Distance(transform.position, collider.transform.position);
        if (distance < nearestDistance)
        {
          nearestPlayer = collider.transform;
          nearestDistance = distance;
        }
      }
    }

    // Nếu có vật thể "Player" gần nhất, di chuyển vật thể của bạn đến gần vật thể đó
    if (nearestPlayer != null)
    {
      return true;
    }
    return false;
  }
  [ClientRpc]
  public void GetHurtClientRpc(int dame, Vector2 pos, int nockBack, ulong id)
  {
    if (!IsOwner) return;
    index.Value = id;
    if (skeletonMovement.HP.Value > 0 && (time >= TIME))
    {
      time = 0;
      animator.SetTrigger(HURT);
      GetComponentInParent<Rigidbody2D>().AddForce(pos.normalized * nockBack, ForceMode2D.Impulse);
      skeletonMovement.HP.Value -= dame;
    }

    if (skeletonMovement.HP.Value <= 0)
    {
      animator.SetTrigger(DEATH);
      Destroy(animator.GetComponent<CapsuleCollider2D>());
    }
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

        o.weapon.increaseTime(5);
      }
    }
  }
  public void SetCantMove()
  {
    if (!IsOwner) return;
    skeletonMovement.canmove = false;
  }
  public void SetCanMove()
  {
    if (!IsOwner) return;
    skeletonMovement.canmove = true;

  }
  public void CreateTrigger()
  {
    subMonsterSword.CreateTrigger();
  }
}
