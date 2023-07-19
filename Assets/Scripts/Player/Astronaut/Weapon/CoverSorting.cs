using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSorting : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cover_sprite;
    
   public void SetCoverOrderON(){
      cover_sprite.sortingOrder = 1;
    }
    public void SetCoverOrderOFF(){
        cover_sprite.sortingOrder = -1;
    }
    
}
