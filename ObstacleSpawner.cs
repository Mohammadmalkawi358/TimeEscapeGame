using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // العقبات والكبسولات
    public Transform[] spawnLanes; // نقاط ولادة في المسارات الثلاثة
    public float spawnInterval = 1.2f;
    public float spawnZStart = 30f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnOne();
            yield return new WaitForSeconds(Mathf.Clamp(spawnInterval - (GameManager.Instance.gameSpeed * 0.01f), 0.5f, 3f));
        }
    }

    void SpawnOne()
    {
        int lane = Random.Range(0, spawnLanes.Length);
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        Vector3 pos = new Vector3(spawnLanes[lane].position.x, spawnLanes[lane].position.y, GameManager.Instance != null ? GameManager.Instance.transform.position.z + spawnZStart : spawnZStart);
        Instantiate(obstaclePrefabs[prefabIndex], pos, Quaternion.identity);
    }
}
