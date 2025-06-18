using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Prefabs de Plataformas")]
    [SerializeField] private GameObject[] platformPrefabs;

    [Header("Índices dos Tipos de Plataforma")]
    [SerializeField] private int normalPlatformIndex = 0;
    [SerializeField] private int movingPlatformIndex = 1;
    [SerializeField] private int breakablePlatformIndex = 2;

    [Header("Posição de Spawn")]
    [SerializeField] private Transform startPlatform;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 4f;

    [Header("Espaçamento Entre Plataformas")]
    [SerializeField] private float gapXMin = 3f;
    [SerializeField] private float gapXMax = 5f;
    [SerializeField] private float gapYMin = -3f;
    [SerializeField] private float gapYMax = 4f;
    [SerializeField] private float gapYMinCluster = 4.5f;
    [SerializeField] private float gapYMaxCluster = 5f;
    [SerializeField] private float clusterGapXMin = -0.8f;
    [SerializeField] private float clusterGapXMax = 0.8f;

    [Header("Probabilidades dos Clusters (%)")]
    [SerializeField] private float chanceCluster1 = 40f;
    [SerializeField] private float chanceCluster2 = 30f;
    [SerializeField] private float chanceCluster3 = 30f;

    [Header("Tempo Entre Spawns")]
    [SerializeField] private float spawnInterval = 1.2f;

    private float spawnTimer;
    private Vector3 lastPlatformPosition;

    private void Start()
    {
        spawnTimer = spawnInterval;
        lastPlatformPosition = startPlatform.position;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnPlatformsGroup();
            spawnTimer = spawnInterval;
        }
    }

    private int GetClusterSize()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < chanceCluster1)
            return 1;
        else if (rand < chanceCluster1 + chanceCluster2)
            return 2;
        else
            return 3;
    }

    private float GetBalancedY(float currentY)
    {
        float bias = 0f;

        if (currentY >= maxY - 1f)
            bias = -1f;
        else if (currentY <= minY + 1f)
            bias = 1f;
        else
            bias = Random.value > 0.5f ? 1f : -1f;

        float yOffset = bias * Random.Range(Mathf.Abs(gapYMin), gapYMax);
        return Mathf.Clamp(currentY + yOffset, minY, maxY);
    }

    private GameObject SelectPlatform(int clusterSize, int platformInGroupIndex, int breakableIndex)
    {
        if (clusterSize == 1 && Random.value <= 0.4f)
        {
            return platformPrefabs[movingPlatformIndex];
        }

        if (clusterSize == 3 && platformInGroupIndex == breakableIndex)
        {
            return platformPrefabs[breakablePlatformIndex];
        }

        return platformPrefabs[normalPlatformIndex];
    }

    private void SpawnPlatformsGroup()
    {
        int clusterSize = GetClusterSize();

        float xOffset = Random.Range(gapXMin, gapXMax);
        float baseX = spawnX + xOffset;

        float baseY = GetBalancedY(lastPlatformPosition.y);

        int breakableIndex = -1;
        if (clusterSize == 3 && Random.value <= 0.7f)
        {
            breakableIndex = Random.Range(0, 3);
        }

        for (int i = 0; i < clusterSize; i++)
        {
            float yOffset = i * Random.Range(gapYMinCluster, gapYMaxCluster);
            yOffset *= Random.value > 0.5f ? 1 : -1;
            float finalY = Mathf.Clamp(baseY + yOffset, minY, maxY);

            float xSpread = Random.Range(clusterGapXMin, clusterGapXMax);
            float finalX = baseX + xSpread;

            Vector3 spawnPosition = new Vector3(finalX, finalY, 0);

            GameObject platformPrefab = SelectPlatform(clusterSize, i, breakableIndex);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }

        lastPlatformPosition = new Vector3(baseX, baseY, 0);
    }
}
