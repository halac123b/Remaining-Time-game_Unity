using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSorting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer weapon_sprite;

 

    public void SetWeaponOrderON(){
      weapon_sprite.sortingOrder = 2;
    }
    public void SetWeaponOrderOFF(){
        weapon_sprite.sortingOrder = -2;
    }

    public void SetWeaponOrderOverOn(){
        weapon_sprite.sortingOrder = 6;
    }
}
