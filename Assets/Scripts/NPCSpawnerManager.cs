using System.Collections;
using UnityEngine;

public class NPCSpawnerManager : MonoBehaviour
{
    [SerializeField] private WanderArea area;
    [SerializeField] private GameObject NPCPrefab;

    [SerializeField] private float spawnIntervalSeconds = 5f;
    [SerializeField] private int maxAliveNPCs = 10;

    private int _aliveCount;
    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(spawnIntervalSeconds);
            if (area == null || NPCPrefab == null)
                continue;
            if (_aliveCount >= maxAliveNPCs)
                continue;
            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        Vector3 spawnPos = area.GetRandomPoint();
        var go = Instantiate(NPCPrefab, spawnPos, Quaternion.identity);

        _aliveCount++;

        var tracker = go.AddComponent<SpawnTracker>();
        tracker.Init(this);
    }

    public void NotifyNPCDestroyed()
    {
        _aliveCount = Mathf.Max(0, _aliveCount - 1); //Mathf to prevent it goes below zero
    }

    private class SpawnTracker : MonoBehaviour
    {
        private NPCSpawnerManager _manager;

        public void Init(NPCSpawnerManager manager)
        {
            _manager = manager;
        }
        private void OnDestroy()
        {
            if (_manager != null)
            {
                _manager.NotifyNPCDestroyed();
            }
        }
    }
}
