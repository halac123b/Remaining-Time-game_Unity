using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class OwnerNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }

        [SerializeField] private SpriteRenderer sprite;
    
        public void SetOrderUp(){
            sprite.sortingOrder = 1;
        }
        public void SetOrderDown(){
            sprite.sortingOrder = -1;
        }
    }

