using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Configuração dos Prefabs")]
    [SerializeField] private GameObject[] platformPrefabs;

    [Header("Posição de Spawn")]
    [SerializeField] private Transform startPlatform;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;

    [Header("Espaçamento Entre Plataformas")]
    [SerializeField] private float gapXMin = 2.5f;
    [SerializeField] private float gapXMax = 4.5f;
    [SerializeField] private float gapYMin = -2.5f;
    [SerializeField] private float gapYMax = 2.5f;

    [Header("Tempo Entre Spawns")]
    [SerializeField] private float spawnInterval = 1.2f;

    private float spawnTimer;
    private Vector3 lastPlatformPosition;

    private void Start()
    {
        spawnTimer = spawnInterval;
        lastPlatformPosition = new Vector3(startPlatform.position.x -3, 0f, 0f);
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
        float yOffset = Random.Range(gapYMin, gapYMax);
        float newY = Mathf.Clamp(lastPlatformPosition.y + yOffset, minY, maxY);

        float xOffset = Random.Range(gapXMin, gapXMax);
        float newX = spawnX + xOffset;

        Vector3 spawnPosition = new Vector3(newX, newY, 0);

        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        Instantiate(prefab, spawnPosition, Quaternion.identity);

        lastPlatformPosition = spawnPosition;
    }
}