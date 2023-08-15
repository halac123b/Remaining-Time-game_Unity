using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItemMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 20;
    private Quaternion rotation;
    public Vector2 moveVector;
    public int damage;
    public int nockBack;
    public ulong idOwner;
    // [SerializeField] Rigidbody2D rigidbody2D;
    public void SetMoveVector(Vector2 movevector){
        moveVector = movevector;
    }

    void Start()
    {
        
        float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // rigidbody2D.AddForce(moveVector*-moveDistance*5000);
    }

    // Update is called once per frame
    void Update()
    {   
        transform.rotation = rotation;
        transform.position += new Vector3(moveVector.x, moveVector.y) * -Speed*Time.deltaTime;
    }
    
     private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.LogError("Vo");
        bool throuth = false;
        Vector2 nockbackVector = new Vector2 (-moveVector.x,-moveVector.y);

        if (other.gameObject.layer ==LayerMask.NameToLayer("Monster")){
            // EquipmentSO equipmentSO = playerEquip.GetEquip(playerAnimator.GetPlayerData().playerWeapon);
            var monsteranimator = other.gameObject.GetComponent<MonsterAnimator>();
            //     if (!equipmentSO) return;
            // var monsteranimator = other.gameObject.GetComponent<MonsterAnimator>();
            var gruntanimator = other.gameObject.GetComponent<SkeletonGruntAnimation>();
            var hunteranimaor = other.gameObject.GetComponent<SkeletonHunterAnimation>();
            // Debug.LogError(monsteranimator +"/"+ gruntanimator +"/"+hunteranimaor);

            // Debug.LogError("i see OnTriggerEnter2D" + NetworkManager.Singleton.LocalClientId);  
            if(monsteranimator){
                if (monsteranimator.time >= MonsterAnimator.TIME){
                    monsteranimator.GetHurtClientRpc(damage,nockbackVector,nockBack,idOwner);
                }else throuth = true;
            } 

            else if(gruntanimator) {
                if  (gruntanimator.time >= SkeletonGruntAnimation.TIME) {
                    gruntanimator.GetHurtClientRpc(damage,nockbackVector,nockBack,idOwner);
                }else throuth =true;
            }

            else if(hunteranimaor){
                 if  (hunteranimaor.time >= SkeletonHunterAnimation.TIME) {
                    hunteranimaor.GetHurtClientRpc(damage,nockbackVector,nockBack,idOwner);
                }else throuth =true;

            }
        }
        else if (other.gameObject.layer ==LayerMask.NameToLayer("Player")){
            if (this.gameObject.layer == LayerMask.NameToLayer("MonsterBullet")) throuth = true;
            if( other.GetComponent<PlayerAnimator>()){
                damage = Mathf.FloorToInt(FindAnyObjectByType<MonsterAnimator>().GetDmg()*1.5f);     
                PlayerAnimator playerAnimator = other.GetComponent<PlayerAnimator>();
                playerAnimator.AstronautHurtClientRpc(damage,nockbackVector,5);
            }
        }
        if (!throuth) Destroy(gameObject);
    }
    
}
