using UnityEngine;
using UnityEngine.Serialization;

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
    [SerializeField] private float gapYMaxPlatformClusters = 5f;
    [SerializeField] private float gapYMinPlatformClusters = 4.5f;
    [SerializeField] private float gapXMaxPlatformClusters = 1f;
    [SerializeField] private float gapXMinPlatformClusters = -1f;

    [Header("Probabilidades dos Clusters (%)")]
    [SerializeField] private float chanceCluster1 = 60f;
    [SerializeField] private float chanceCluster2 = 20f;
    [SerializeField] private float chanceCluster3 = 20f;

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
            SpawnPlatformsGroup();
            spawnTimer = spawnInterval;
        }
    }
    
    private int GetClusterSize()
    {
        float rand = Random.Range(0f, 100f);

        if (rand < chanceCluster1)
        {
            return 1;
        }
        else if (rand < chanceCluster1 + chanceCluster2)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    
    private float GetBalancedY(float currentY)
    {
        float bias = 0f;

        // Checa se está muito alto
        if (currentY >= maxY - 1f)
        {
            // Força pra baixo
            bias = -1f;
        }
        // Checa se está muito baixo
        else if (currentY <= minY + 1f)
        {
            // Força pra cima
            bias = 1f;
        }
        else
        {
            // Caso normal, sorteia subir ou descer
            bias = Random.value > 0.5f ? 1f : -1f;
        }

        float yOffset = bias * Random.Range(Mathf.Abs(gapYMin), gapYMax);
        float finalY = Mathf.Clamp(currentY + yOffset, minY, maxY);

        return finalY;
    }

    private void SpawnPlatformsGroup()
    {
        int platformsInGroup = GetClusterSize();

        float xOffset = Random.Range(gapXMin, gapXMax);
        float baseX = spawnX + xOffset;

        float baseY = GetBalancedY(lastPlatformPosition.y);

        for (int i = 0; i < platformsInGroup; i++)
        {
            // Variação dentro do grupo no eixo Y
            float yOffset = i * Random.Range(gapYMinPlatformClusters, gapYMaxPlatformClusters);
            yOffset *= Random.value > 0.5f ? 1 : -1;
            float finalY = Mathf.Clamp(baseY + yOffset, minY, maxY);

            // Variação no eixo X dentro do grupo
            float xSpread = Random.Range(gapXMinPlatformClusters, gapXMaxPlatformClusters);
            float finalX = baseX + xSpread;

            Vector3 spawnPosition = new Vector3(finalX, finalY, 0);

            GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }

        lastPlatformPosition = new Vector3(baseX, baseY, 0);
    }
}