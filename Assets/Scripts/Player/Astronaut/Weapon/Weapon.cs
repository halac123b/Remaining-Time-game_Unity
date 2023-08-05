using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Unity.Netcode;
using Mono.Cecil.Cil;
using Unity.VisualScripting;

public class Weapon : NetworkBehaviour
{   
    
    [SerializeField] public GameObject FloatingText;
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerAnimator playerAnimator;
    private PlayerEquip playerEquip;
    private Sprite sprite;
    public int count =0;
    void Start()
    {  
        playerEquip = FindAnyObjectByType<PlayerEquip>();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    void LateUpdate()
    {   
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        if (playerAnimator.GetPlayerData().playerWeapon <3 && count == ((playerAnimator.GetPlayerData().playerWeapon == 0 )?(10):(15)) ){
            // count =0;
            float x = playerAnimator.animator.GetFloat("Horizontal");
            float y = playerAnimator.animator.GetFloat("Vertical");
            if (Mathf.Abs(y) >0.1f) x = 0;
            var trigg=gameObject.AddComponent<PolygonCollider2D>();
            trigg.isTrigger = true;
            trigg.offset += new Vector2(-Mathf.Abs(x),y);
            // Debug.LogError("(x,y): "+x+" "+y);
        }
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   

        if (other){
            // Debug.LogError("Trigger: " + playerAnimator.GetPlayerData().playerWeapon);    
            Vector2 attackVector = new Vector2((playerAnimator.animator.GetFloat("Vertical")==0)?playerAnimator.animator.GetFloat("Horizontal"):0,playerAnimator.animator.GetFloat("Vertical"));
            
            if (playerAnimator.GetPlayerData().playerWeapon<3 && playerAnimator.GetPlayerData().playerWeapon>-1 && other.gameObject.layer ==LayerMask.NameToLayer("Monster")){
                EquipmentSO equipmentSO = playerEquip.GetEquip(playerAnimator.GetPlayerData().playerWeapon);
                if (!equipmentSO) return;
                var monsteranimator = other.GetComponent<MonsterAnimator>();
                var gruntanimator = other.GetComponent<SkeletonGruntAnimation>();
                var hunteranimaor = other.GetComponent<SkeletonHunterAnimation>();
                // Debug.LogError(monsteranimator +"/"+ gruntanimator +"/"+hunteranimaor);

                // Debug.LogError("i see OnTriggerEnter2D" + NetworkManager.Singleton.LocalClientId);  

                if(monsteranimator && monsteranimator.time >= MonsterAnimator.TIME) monsteranimator.GetHurtClientRpc(equipmentSO.damage,attackVector,equipmentSO.nockBack,playerAnimator.GetPlayerData().Id);
                else if(gruntanimator && gruntanimator.time >= SkeletonGruntAnimation.TIME) gruntanimator.GetHurtClientRpc(equipmentSO.damage,attackVector,equipmentSO.nockBack,playerAnimator.GetPlayerData().Id);
                else if(hunteranimaor && hunteranimaor.time >= SkeletonHunterAnimation.TIME) hunteranimaor.GetHurtClientRpc(equipmentSO.damage,attackVector,equipmentSO.nockBack,playerAnimator.GetPlayerData().Id);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogError("Collider");    
    }

    public void increaseTime(int time){
        if(!IsOwner) return;
        var playerstatus = FindAnyObjectByType<PlayerStatus>();
        playerstatus.SetTimeLeft(playerstatus.GetTimeLeft()+time);
    }
}
