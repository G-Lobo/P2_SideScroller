using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyX = -15f;
    [SerializeField] private float destroyYMin = -7f;
    [SerializeField] private float destroyYMax = 7f;

    void Update()
    {
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

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
