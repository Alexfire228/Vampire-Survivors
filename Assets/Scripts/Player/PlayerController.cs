using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private Vector3 inputDir;
    private Rigidbody2D rgb;

    [SerializeField] private float xSpeed;
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        CheckKeys();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void CheckKeys()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    private void Move()
    {
        rgb.velocity = inputDir * xSpeed;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner)
            return;

        PlayerCamera.Instance.MoveCameraToPlayer(this.transform);
    }
}
