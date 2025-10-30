using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Canvas networkCanvas;

    [SerializeField] private NetworkManager networkManager;

    void Start()
    {
        ShowNetwork(true);
    }

    public void ShowNetwork(bool show)
    {
        networkCanvas.enabled = show;
    }

    public void Host()
    {
        networkManager.StartHost();
    }

    public void Server()
    {
        networkManager.StartServer();
    }

    public void Client()
    {
        networkManager.StartClient();
    }
}
