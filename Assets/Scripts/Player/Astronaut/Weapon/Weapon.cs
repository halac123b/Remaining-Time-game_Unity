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
    [SerializeField] PlayerAnimator playerAnimator;
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
            var trigg=gameObject.AddComponent<PolygonCollider2D>();
            trigg.isTrigger = true;
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


                if(monsteranimator) monsteranimator.GetHurt(equipmentSO.damage,attackVector,equipmentSO.nockBack);
                else if(gruntanimator) gruntanimator.GetHurt(equipmentSO.damage,attackVector,equipmentSO.nockBack,this);
                else if(hunteranimaor) hunteranimaor.GetHurt(equipmentSO.damage,attackVector,equipmentSO.nockBack,this);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogError("Collider");    
    }

    public void increateTime(int time){
        // GameObject floatingtext = Instantiate(FloatingText,new Vector3(playerAnimator.transform.position.x,playerAnimator.transform.position.y + 1.5f), Quaternion.identity,playerAnimator.transform);
        if(!IsOwner) return;
        
        // NetworkObject newGameObjectNetworkObject = newGameObject.GetComponent<NetworkObject>();
        // newGameObjectNetworkObject.SpawnWithOwnership(playerAnimator.GetPlayerData().Id, true);

        var playerstatus = FindAnyObjectByType<PlayerStatus>();
        playerstatus.SetTimeLeft(playerstatus.GetTimeLeft()+time);
    }
}
