using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private bool hasAccess = false;

    void Start()
    {
        TryToWork();
    }

    void TryToWork()
    {
        hasAccess = SemaphoreManager.Instance.TryEnter(gameObject);
        if (hasAccess)
        {
            StartCoroutine(WorkAndExit());
        }
    }

    public void GrantAccess()
    {
        hasAccess = true;
        StartCoroutine(WorkAndExit());
    }

    IEnumerator WorkAndExit()
    {
        // Simulate work (e.g., picking up an item)
        yield return new WaitForSeconds(3f);

        // Exit semaphore
        SemaphoreManager.Instance.Exit();
    }
}
