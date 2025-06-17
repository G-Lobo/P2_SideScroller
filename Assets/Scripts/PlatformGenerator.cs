using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Configuração dos Prefabs")]
    [SerializeField] private GameObject[] platformPrefabs;

    [Header("Configuração do Spawn")]
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    [Header("Tempo entre spawns")]
    [SerializeField] private float spawnInterval = 1.2f; // Ajusta esse valor para mais frequente ou menos

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
        
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnPlatform();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnPlatform()
    {
        float yPos = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(spawnX, yPos, 0);

        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}