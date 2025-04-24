using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour
{
    private Animator animator;
    float wait;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        
        StartWork();
    }
    public void StartWork()
    {
        gameObject.SetActive(true); // Activate worker
                                    //animator.SetTrigger("StartWorking"); // Play working animation
      //  wait = Random.Range(5f, 10f);
      //  StartCoroutine(WorkRoutine());
    }

    IEnumerator WorkRoutine()
    {
        
        yield return new WaitForSeconds(wait); // Simulate work time

       // animator.SetTrigger("StopWorking"); // Optional: Play exit animation

        yield return new WaitForSeconds(0.5f); // Short delay before disappearing
      //  gameObject.SetActive(false); // Disable worker after work is done

        SemaphoreManager.Instance.WorkerFinished(gameObject);
    }
}
