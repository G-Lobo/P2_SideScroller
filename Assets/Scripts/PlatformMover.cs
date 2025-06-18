using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyX = -15f;
    [SerializeField] private float destroyYMin = -6f;
    [SerializeField] private float destroyYMax = 5.5f;

    void Update()
    {
        // Movimento constante para a esquerda
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        // Se sair da tela pela esquerda, destrói
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }

        if (transform.position.y > destroyYMax || transform.position.y < destroyYMin)
        {
            Destroy(gameObject);
        }
    }
}
