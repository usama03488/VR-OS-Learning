using UnityEngine;
using System.Collections.Generic;

public class SemaphoreManager : MonoBehaviour
{
    public int maxWorkers = 3; // Semaphore value
    private Queue<GameObject> waitingWorkers = new Queue<GameObject>();
    private List<GameObject> activeWorkers = new List<GameObject>();

    public List<GameObject> workers = new List<GameObject>(); // Worker prefabs
    public List<Transform> spawnPoints = new List<Transform>(); // Spawn positions

    public static SemaphoreManager Instance; // Singleton for easy access

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWorkers();
        ActivateInitialWorkers();
    }

    void SpawnWorkers()
    {
        foreach (Transform point in spawnPoints)
        {
            int index = Random.Range(0, workers.Count);
            GameObject obj = Instantiate(workers[index], point.position, point.rotation);
            obj.SetActive(true);
            waitingWorkers.Enqueue(obj);
        }
    }

    void ActivateInitialWorkers()
    {
        //for (int i = 0; i < maxWorkers; i++)
        //{
        //    ActivateNextWorker();
        //}
    }

    public void WorkerFinished(GameObject worker)
    {
        activeWorkers.Remove(worker);
        worker.SetActive(false);
        waitingWorkers.Enqueue(worker);

        // Immediately replace it if the semaphore allows
        ActivateNextWorker();
    }

    private void ActivateNextWorker()
    {
        if (activeWorkers.Count < maxWorkers && waitingWorkers.Count > 0)
        {
            GameObject nextWorker = waitingWorkers.Dequeue();
            nextWorker.SetActive(true);
            activeWorkers.Add(nextWorker);
            nextWorker.GetComponent<Worker>().StartWork();
        }
    }

    public void UpdateSemaphoreValue(float newValue)
    {
        maxWorkers = Mathf.RoundToInt(newValue);

        // Adjust active workers to match new semaphore value
        //while (activeWorkers.Count < maxWorkers)
        //{
        //    ActivateNextWorker();
        //}

        //while (activeWorkers.Count > maxWorkers)
        //{
        //    GameObject worker = activeWorkers[activeWorkers.Count - 1];
        //    activeWorkers.Remove(worker);
        //    worker.SetActive(false);
        //    waitingWorkers.Enqueue(worker);
        //}
    }
}
