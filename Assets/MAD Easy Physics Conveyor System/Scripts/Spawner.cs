using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using System.Collections;
// Starting in 2 seconds.
// a projectile will be launched every 3 seconds

namespace MADFerret
{
    public class Spawner : MonoBehaviour
    {
        public PC_Problem problemManager;
        public Animator animator;
        public Rigidbody[] Box;
        public int BoxNo;
        public bool IsSemaphore = false;
        public bool IsLimit;
        public List<SlotManager> slots; // Assign 3 SlotManager references in Inspector
        public float spawnInterval = 1f;
        public FlipperController flipper1;
        public FlipperController flipper2;
        public float initialSpawnDelay = 1.5f;
        public float spawnDelayBetweenBoxes = 2f;

        private bool isWaitingToSpawn = false;
        private void Start()
        {
            foreach (var slot in slots)
            {
                slot.OnSlotFreed += HandleSlotFreed;
                slot.OnSlotOccupied += CheckSlots;
            }

           FirstSpawn();
        }
        public void CheckSlots(SlotManager slot)
        {
            TrySpawnBox();

        }
        //void Start()
        //{
        //    InvokeRepeating("SpawnBox", 2.0f, 5f);
        //}


        //private void HandleSlotFreed(SlotManager freedSlot)
        //{
        //    TrySpawnBox(); // Try to spawn a new box for the freed slot
        //}
        private IEnumerator DelayedTrySpawnBox()
        {
            isWaitingToSpawn = true;
            yield return new WaitForSeconds(spawnDelayBetweenBoxes);
            TrySpawnBox();
            isWaitingToSpawn = false;
        }
        void FirstSpawn()
        {

            if (!slots[0].isOccupied)
            {
                SetFlipperPathToSlot(0);
                SpawnBoxTo(slots[0]);
            }
        }
        private void TrySpawnBox()
        {
            
                if (!slots[0].isOccupied)
                {
                    SetFlipperPathToSlot(0);
                    SpawnBoxTo(slots[0]);
                }
                else if (!slots[1].isOccupied)
                {
                    SetFlipperPathToSlot(1);
                    SpawnBoxTo(slots[1]);
                }
                else if (!slots[2].isOccupied)
                {
                    SetFlipperPathToSlot(2);
                    SpawnBoxTo(slots[2]);
                }
                else
                {
                    Debug.Log("All slots full. No spawning.");
                }
            
        }
        private IEnumerator DelayedInitialSpawn()
        {
            yield return new WaitForSeconds(1.5f); // 👈 Delay time (adjust as needed)
            TrySpawnBox();
        }
        private void SpawnBoxTo(SlotManager slot)
        {
            GameObject box = Instantiate(Box[0].gameObject,transform.position,transform.rotation);
           // slot.ReceiveBox(box);
        }

        private void SetFlipperPathToSlot(int slotIndex)
        {
            switch (slotIndex)
            {
                case 0: // Slot 1
                    flipper1.RotateTo(35f); // Send box to Slot 1
                    break;
                case 1: // Slot 2
                        //  flipper1.RotateTo(0f);  // Go straight
                    flipper1.RotateTo(-35f);
                    flipper2.RotateTo(-20f);
                    break;
                case 2: // Slot 3
                    flipper1.RotateTo(-35f);  // Go straight
                    flipper2.RotateTo(20f);
                    break;
            }
        }
        void SpawnBox()
        {

           if (!IsSemaphore &&problemManager.isFull == false)
            {
                problemManager.IncrementAmount();
                BoxNo = Random.Range(0, Box.Length);
                Instantiate(Box[BoxNo], transform.position, transform.rotation);
            }
           else if (IsSemaphore)
            {
               
                BoxNo = Random.Range(0, Box.Length);
                Instantiate(Box[BoxNo], transform.position, transform.rotation);
            }
               
            
           
        
        }
        private void HandleSlotFreed(SlotManager freedSlot)
        {
            if (!isWaitingToSpawn)
            {
                StartCoroutine(DelayedTrySpawnBox());
            }
        }
        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                SlotManager freeSlot = GetFreeSlot();

                if (freeSlot != null)
                {
                    GameObject box = Instantiate(Box[0].gameObject);
                    freeSlot.ReceiveBox(box);
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private SlotManager GetFreeSlot()
        {
            foreach (var slot in slots)
            {
                if (!slot.isOccupied)
                    return slot;
            }
            return null; // All occupied
        }
    }
}