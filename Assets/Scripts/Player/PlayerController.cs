using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    private Vector3 inputDir;
    private Rigidbody2D rgb;

    [SerializeField] private float xSpeed;

    [SerializeField] private GameObject meteor;

    public static PlayerController Instance;
    void Start()
    {
        Instance = this;
        rgb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        CheckKeys();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(meteor);
        }
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
