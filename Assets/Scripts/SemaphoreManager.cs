using UnityEngine;
using System.Collections.Generic;

public class SemaphoreManager : MonoBehaviour
{
    public int maxWorkers = 3; // Semaphore value
    private Queue<GameObject> waitingWorkers = new Queue<GameObject>();
    private List<GameObject> activeWorkers = new List<GameObject>();

    public List<GameObject> workers = new List<GameObject>(); // Worker prefabs
    public List<Transform> spawnPoints = new List<Transform>(); // Spawn positions
    public List<SlotManager> managers;
    public List<Worker> Worker_manager;
    public bool IsOldVersion;
    public static SemaphoreManager Instance; // Singleton for easy access

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (IsOldVersion)
        {
            SpawnWorkers(false);
        }
        else
        {
            SpawnWorkers(true);
            ActivateInitialWorkers();
        }
    
    }
    public void FreeAll_Workers()
    {
        for (int i = 0; i < managers.Count; i++)
        {
            managers[i].InstantFree();
        }
    }
    void SpawnWorkers(bool status)
    {
        if (status == false)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                int index = Random.Range(0, workers.Count);
                GameObject obj = Instantiate(workers[index], spawnPoints[i].position, spawnPoints[i].rotation);
                Debug.Log("Assigned");
                Worker_manager.Add(obj.GetComponent<Worker>());
               // Worker_manager[i]= obj.GetComponent<Worker>();
                obj.SetActive(status);
                waitingWorkers.Enqueue(obj);
            }
        }
        else
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                int index = Random.Range(0, workers.Count);
                GameObject obj = Instantiate(workers[index], spawnPoints[i].position, spawnPoints[i].rotation);
                managers[i].worker = obj.GetComponent<Worker>();
                obj.SetActive(status);
                waitingWorkers.Enqueue(obj);
            }
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
    private bool isUpdating = false;
    public void UpdateSemaphoreValue(float newValue)
    {
        if (isUpdating) return;
        isUpdating = true;

        maxWorkers = Mathf.RoundToInt(newValue);

        // Increase active workers if needed
        while (activeWorkers.Count < maxWorkers && waitingWorkers.Count > 0)
        {
            ActivateNextWorker();
        }

        // Decrease active workers if needed
        while (activeWorkers.Count > maxWorkers)
        {
            GameObject worker = activeWorkers[activeWorkers.Count - 1];
            activeWorkers.RemoveAt(activeWorkers.Count - 1);
            worker.SetActive(false);
            waitingWorkers.Enqueue(worker);
        }

        isUpdating = false;
    }
}
