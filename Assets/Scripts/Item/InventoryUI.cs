using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerItem playerItem;
    [SerializeField] GameObject UI;
    [SerializeField] Sprite transperent;
    public class Inventory
  {
    public ItemSO item;
    public string number;
   
  }

    private List<Inventory> inventories = new List<Inventory>();
    private List<Button> buttons = new List<Button>();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerItem = FindAnyObjectByType<PlayerItem>();
        Button[] buttons_t =  UI.gameObject.GetComponentsInChildren<Button>();
        
        foreach (var item in buttons_t){
            buttons.Add(item);
        }

        for (int i = 0 ; i < 15; i++){
            inventories.Add(new Inventory{
                item= null,
                number = ""
            });
        }
        
        for (int i = 0 ; i < buttons.Count; i++){

             buttons[i].GetComponentsInChildren<Image>()[1].sprite = (inventories[i].item == null)?transperent:inventories[i].item.GetSprite();
             buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = inventories[i].number;
        }
        int index = 0;
        buttons[index].onClick.AddListener(() =>
        {  
            for(int j=3; j < 15;j++){
                if(inventories[j].item==null){
                    SwapItem(0,j);
                    break;
                }
            }
        });
        index++;  
        buttons[index].onClick.AddListener(() =>
        {  
            for(int j=3; j < 15;j++){
                if(inventories[j].item==null){
                   SwapItem(1,j);
                    break;
                }
            }
        });
        index++;  
        buttons[index].onClick.AddListener(() =>
        {  
            for(int j=3; j < 15;j++){
                if(inventories[j].item==null){
                    SwapItem(2,j);
                    break;
                }
            }
        });
        index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(3,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(4,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(5,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                   SwapItem(6,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(7,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(8,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(9,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(10,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(11,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(12,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(13,j);
                    break;
                }
            }
        });
         index++;
        buttons[index].onClick.AddListener(() =>
        {
                for(int j=0; j < 3;j++){
                if(inventories[j].item==null){
                    SwapItem(14,j);
                    break;
                }
            }
        });
    }


    // Debug.LogError("Inventory Capacity: "+ texts.Length);
    

   
    void Start()
    {
        UpdateInventory();
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventory();
        for (int i = 0 ; i < buttons.Count; i++){
            buttons[i].GetComponentsInChildren<Image>()[1].sprite =(inventories[i].item == null)?transperent:inventories[i].item.GetSprite();
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = inventories[i].number;
            if (inventories[i].number == "0") {
                buttons[i].GetComponentsInChildren<Image>()[1].sprite = transperent;
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text="";
            }
        }
    }

    private void SwapItem(int x, int y){
        ItemSO tempSO = inventories[x].item;
        string tempNumber = inventories[x].number;
        inventories[x].item = inventories[y].item;
        inventories[x].number = inventories[y].number;
        inventories[y].item = tempSO;
        inventories[y].number = tempNumber;
    }

    public void UpdateInventory(){
        for (int i =0 ; i< playerItem.itemList.Count;i++){
            bool have = false;
            for (int j = 0 ; j < buttons.Count ;j++){
                if (inventories[j].item !=null && playerItem.itemList[i].item.GetName() == inventories[j].item.GetName()){
                    inventories[j].item = playerItem.itemList[i].item;
                    inventories[j].number = playerItem.itemList[i].number.ToString();
                    have=true;
                    break;
                }
            }
            if(!have){
                for (int j = 0 ; j < buttons.Count ;j++){
                    if (inventories[j].item == null){
                        inventories[j].item = playerItem.itemList[i].item;
                        inventories[j].number = playerItem.itemList[i].number.ToString();
                         break;
                    }
                }
            }
        }
    }

    public Inventory GetInventory(int index){
        return inventories[index];
    }
}
