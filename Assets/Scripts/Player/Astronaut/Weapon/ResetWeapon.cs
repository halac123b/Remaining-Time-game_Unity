using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ResetWeapon : MonoBehaviour
{
    
    [SerializeField] PlayerAnimator playerAnimator;
    private PlayerEquip playerEquip;
    private Sprite sprite;
    void Start()
    {  
        playerEquip = FindAnyObjectByType<PlayerEquip>();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    void LateUpdate()
    {   
        
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        if (gameObject.GetComponent<SpriteRenderer>().sprite != sprite){
            sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            var trigg=gameObject.AddComponent<PolygonCollider2D>();
            trigg.isTrigger = true;
        }
    }
    private void Update()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        // Debug.LogError("Trigger: " + playerAnimator.GetPlayerData().playerWeapon);    
        if (playerAnimator.GetPlayerData().playerWeapon<3 && playerAnimator.GetPlayerData().playerWeapon>-1 && other.gameObject.layer ==LayerMask.NameToLayer("Monster")){
            EquipmentSO equipmentSO = playerEquip.GetEquip(playerAnimator.GetPlayerData().playerWeapon);
            
            if(other.GetComponent<MonsterAnimator>()) other.GetComponent<MonsterAnimator>().GetHurt(equipmentSO.damage,transform.position,equipmentSO.nockBack);
            if(other.GetComponentInChildren<SkeletonGruntAnimation>()) other.GetComponentInChildren<SkeletonGruntAnimation>().GetHurt(equipmentSO.damage,equipmentSO.nockBack);
            if(other.GetComponentInChildren<SkeletonHunterAnimation>()) other.GetComponentInChildren<SkeletonHunterAnimation>().GetHurt(equipmentSO.damage,equipmentSO.nockBack);

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogError("Collider");    
    }
}
