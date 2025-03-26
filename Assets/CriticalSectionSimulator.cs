using UnityEngine;
using TMPro;
using System.Collections; // For displaying the shared resource value
using UnityEngine.InputSystem;
public class CriticalSectionSimulator : MonoBehaviour
{
    //public TextMeshProUGUI sharedValueText; // UI Text to show shared resource value
    private int sharedResource = 5; // Shared variable (represents the machine’s state)
    public TMP_Text Code_A ;
    public TMP_Text Code_B ;

    private bool isPlayerUsing = false;
    private bool isOtherWorkerUsing = false;
    bool IsRaceCondiiton = false;
   
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && isPlayerUsing==false && isOtherWorkerUsing==false)
        {
            StartCoroutine(PlayerUseMachine(1));
            StartCoroutine(OtherWorkerUseMachine(-1));
        }
        // Display the current shared value
      //  sharedValueText.text = "Shared Resource: " + sharedResource;

        // Simulate race condition
        if (isPlayerUsing && isOtherWorkerUsing && va1 - va2 > 1)
        {
            va1 = 0;
            va2 = 0;
              // Both accessing at the same time - causes inconsistency
            Code_A.text = Code_A.text+ " Race Condition";
            Code_B.text = Code_B.text+ " Race Condition";
            Debug.Log("Race Condition! Both workers are using the machine.");
            IsRaceCondiiton=true;
          
        }
    }
    int va1, va2 = 0;
    private IEnumerator PlayerUseMachine(int change)
    {
     
        isPlayerUsing = true;
        int temp = sharedResource; // Both get the same initial value

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); // Simulate unpredictable execution timing

        temp += change; // Modify the temp value
        sharedResource = temp; // Write back the modified value
        va1 = temp;
            Code_A.text = "Output: " + temp;

        if (IsRaceCondiiton == false)
        {
            Code_A.text = "Output: " + temp + " Race Condition";
        }
       
       // Debug.Log(workerName + " sees sharedResource as: " + temp);
    }
    private IEnumerator OtherWorkerUseMachine(int change)
    {
       
        isOtherWorkerUsing = true;
        int temp = sharedResource; // Both get the same initial value

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); // Simulate unpredictable execution timing

        temp += change; // Modify the temp value
        sharedResource = temp; // Write back the modified value
        va2=temp;
        if (IsRaceCondiiton == false)
        {
            Code_B.text = "Output: " + temp;
        }
        else
        {
            Code_B.text = "Output: " + temp + " Race Condition";
        }
        // Debug.Log(workerName + " sees sharedResource as: " + temp);
    }
    // Called when the player interacts with the machine
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
        IsRaceCondiiton = false;
        sharedResource = 5;
        ResetPlayer();
        ResetOtherWorker();
    }
    void ResetPlayer()
    {
        Code_A.text = "";
        isPlayerUsing = false;
    }

    void ResetOtherWorker()
    {
        Code_B.text = "";
        isOtherWorkerUsing = false;
    }
}
