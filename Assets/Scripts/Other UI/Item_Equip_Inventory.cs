using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Equip_Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] public GameObject inventoryUI;
    public bool show_inventory = false;

    private void Awake()
    {
        
        
    }

    void Start()
    {
        // inventoryUI.SetActive(false);
        show_inventory = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        inventoryUI.SetActive(show_inventory);
    }
}
