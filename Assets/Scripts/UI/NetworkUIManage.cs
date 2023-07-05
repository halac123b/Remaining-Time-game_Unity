using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button server;
    [SerializeField] private Button host;
    [SerializeField] private Button client;

    private void Awake()
    {
        server.onClick.AddListener(()=> {
            NetworkManager.Singleton.StartServer();
        });
        host.onClick.AddListener(()=> {
            NetworkManager.Singleton.StartHost();
        });   
        client.onClick.AddListener(()=> {
            NetworkManager.Singleton.StartClient();
        });      
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
