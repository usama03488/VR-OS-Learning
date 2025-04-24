using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CriticalSectionSimulator : MonoBehaviour
{
    public TMP_Text Code_A;
    public TMP_Text Code_B;

    private int sharedResource = 5;

    private bool isPlayerUsing = false;
    private bool isOtherWorkerUsing = false;
    private bool isRaceCondition = false;

    private bool playerActive = false;  // Track if Player coroutine is running
    private bool workerActive = false;  // Track if Worker coroutine is running

    int va1 = 0, va2 = 0;

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && !isPlayerUsing && !isOtherWorkerUsing)
        {
            StartCoroutine(PlayerUseMachine(1));
            StartCoroutine(OtherWorkerUseMachine(-1));
        }
    }

    private IEnumerator PlayerUseMachine(int change)
    {
        isPlayerUsing = true;
        playerActive = true;  // Mark player coroutine as active

        int temp = sharedResource;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        // Race condition occurs if worker is also modifying sharedResource at the same time
        if (workerActive)
        {
            isRaceCondition = true;
        }

        temp += change;
        sharedResource = temp;
        va1 = temp;

        Code_A.text = isRaceCondition ? "Output: " + temp + " Race Condition" : "Output: " + temp;

        playerActive = false;  // Mark coroutine as completed
        isPlayerUsing = false;
    }

    private IEnumerator OtherWorkerUseMachine(int change)
    {
        isOtherWorkerUsing = true;
        workerActive = true;  // Mark worker coroutine as active

        int temp = sharedResource;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        // Race condition occurs if player is also modifying sharedResource at the same time
        if (playerActive)
        {
            isRaceCondition = true;
        }

        temp += change;
        sharedResource = temp;
        va2 = temp;

        Code_B.text = isRaceCondition ? "Output: " + temp + " Race Condition" : "Output: " + temp;

        workerActive = false;  // Mark coroutine as completed
        isOtherWorkerUsing = false;
    }
    public void PlayerUseMachine()
    {
        StartCoroutine(PlayerUseMachine(1));

        //isPlayerUsing = true;
        //sharedResource++; // Increment shared resource
        //Debug.Log("Player is using the machine.");
        //Code_A.text = "Output:" + sharedResource ;
        // Invoke("ResetPlayer", 1f); // Simulate short usage time

    }

    // Called when the other worker button is pressed
    public void OtherWorkerUseMachine()
    {
        StartCoroutine(OtherWorkerUseMachine(-1));
        //isOtherWorkerUsing = true;
        //sharedResource--; // Increment shared resource
        //Debug.Log("Other worker is using the machine.");
        //Code_B.text = "Output:" + sharedResource ;
        // Invoke("ResetOtherWorker", 1f); // Simulate short usage time
    }
    public void Reset()
    {
        isRaceCondition = false;
        sharedResource = 5;
        ResetPlayer();
        ResetOtherWorker();
    }

    void ResetPlayer()
    {
        Code_A.text = "";
        isPlayerUsing = false;
        playerActive = false;
    }

    void ResetOtherWorker()
    {
        Code_B.text = "";
        isOtherWorkerUsing = false;
        workerActive = false;
    }
}

