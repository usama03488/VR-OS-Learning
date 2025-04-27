using System;
using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public bool isOccupied = false;
    public float occupiedDuration = 5f; // Time to free the slot

    // Event for notifying when this slot becomes free
    public event Action<SlotManager> OnSlotFreed;
    public event Action<SlotManager> OnSlotOccupied;
    public GameObject Box;
    public Worker worker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
            Debug.Log("bag c");
            ReceiveBox(other.gameObject);

        }
    }
    public void ReceiveBox(GameObject box)
    {
        worker.status.text = "Occupied";
        isOccupied = true;
        box.transform.position = transform.position;
        Box= box;
        OnSlotOccupied?.Invoke(this);
        StartCoroutine(FreeAfterDelay());
    }

    private IEnumerator FreeAfterDelay()
    {
        yield return new WaitForSeconds(occupiedDuration);
        isOccupied = false;
        worker.status.text = "Available";
        Destroy(Box);
        // Notify listeners that this slot is now free
        OnSlotFreed?.Invoke(this);
    }
}
