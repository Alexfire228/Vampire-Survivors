using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    private Transform linkToPlayer;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        transform.position = linkToPlayer.position - new Vector3(0, 0, 10);
    }

    public void MoveCameraToPlayer(Transform player)
    {
        linkToPlayer = player;
    }
}
