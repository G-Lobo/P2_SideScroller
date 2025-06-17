using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyX = -15f;  // Limite da esquerda onde a plataforma é destruída

    void Update()
    {
        // Movimento constante para a esquerda
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        // Se sair da tela pela esquerda, destrói
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
