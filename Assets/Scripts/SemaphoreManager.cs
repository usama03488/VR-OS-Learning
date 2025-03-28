using UnityEngine;
using System.Collections.Generic;

public class SemaphoreManager : MonoBehaviour
{
    public int maxWorkers = 3; // Semaphore value
    private Queue<GameObject> waitingWorkers = new Queue<GameObject>();
    private int currentWorkers = 0;

    public static SemaphoreManager Instance; // Singleton for easy access

    private void Awake()
    {
        Instance = this;
    }

    public bool TryEnter(GameObject worker)
    {
        if (currentWorkers < maxWorkers)
        {
            currentWorkers++;
            return true; // Access granted
        }
        else
        {
            waitingWorkers.Enqueue(worker);
            return false; // Must wait
        }
    }

    public void Exit()
    {
        currentWorkers--;
        if (waitingWorkers.Count > 0)
        {
            GameObject nextWorker = waitingWorkers.Dequeue();
            nextWorker.GetComponent<Worker>().GrantAccess();
        }
    }

    public void UpdateSemaphoreValue(float newValue)
    {
        maxWorkers = (int)newValue;
        while (currentWorkers < maxWorkers && waitingWorkers.Count > 0)
        {
            GameObject nextWorker = waitingWorkers.Dequeue();
            nextWorker.GetComponent<Worker>().GrantAccess();
            currentWorkers++;
        }
    }
}
