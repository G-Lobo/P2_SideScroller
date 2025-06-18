using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Configuração de Movimento Vertical")]
    [SerializeField] private float amplitude = 1.5f; 
    [SerializeField] private float frequency = 2f;  

    private Vector3 startPos;

    private void Start()
    {
       
        startPos = transform.position;
    }

    private void Update()
    {
      
        float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(transform.position.x, startPos.y + offsetY, transform.position.z);
    }
}