using System.Collections;
using TMPro;
using UnityEngine;

public class Atm : MonoBehaviour
{
    public ATMQueueManager manager;
    public GameObject workingCharacter;
    public TMP_Text Status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "npc" && isStarted==false)
        {
            workingCharacter = other.gameObject;
            StartWork();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    bool isStarted = false;
    public void StartWork()
    {
        isStarted = true;
        Status.text = "Occupied";
        StartCoroutine(FinishWork());

    }
    IEnumerator FinishWork()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
       Destroy(workingCharacter);
      isStarted = false;
        Status.text = "Available";
        manager.canpress = true;
    }
}
