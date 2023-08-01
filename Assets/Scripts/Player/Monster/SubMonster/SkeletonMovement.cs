using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkeletonMovement : NetworkBehaviour
{
    public bool canmove = true;
    public NetworkVariable<float> HP =  new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    void Update()
    {
        // Tìm tất cả các vật thể trong bán kính 10 đơn vị xung quanh vật thể của bạn
         if(canmove && HP.Value > 0)   transform.position += Getdirection() * Time.deltaTime * 3f;
        
    }
    /////////////////////Support//////////////////////////

    public Vector3 Getdirection(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);

        // Lọc ra vật thể gần nhất có layer là "Player"
        Transform nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
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
            Vector3 direction = (nearestPlayer.position - transform.position).normalized;
            return direction;
        }
        return new Vector3();
    }
}
