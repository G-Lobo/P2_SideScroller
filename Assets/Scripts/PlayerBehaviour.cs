using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Camera playerCamera;
    [SerializeField] private Collider2D playerCollider;

    private float gameScrollSpeed = 0.1f;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerRb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        //movimentaçao da camera para a direita
        playerCamera.transform.position = new Vector3(playerCamera.transform.position.x + gameScrollSpeed, playerCamera.transform.position.y, playerCamera.transform.position.z);

        //movimentaçao do player em relaçao a camera
        playerRb.transform.position = new Vector3(playerRb.transform.position.x + gameScrollSpeed, playerRb.transform.position.y, playerRb.transform.position.z);
    }
}
