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
        Destroy(Box);
        // Notify listeners that this slot is now free
        OnSlotFreed?.Invoke(this);
    }
}
