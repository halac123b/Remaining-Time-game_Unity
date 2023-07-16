using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarry : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponcarry;
    
    public void SetActive(){
        weaponcarry.gameObject.SetActive(true);
    }   

    public void SetDisable(){
        weaponcarry.gameObject.SetActive(false);
    } 
}
