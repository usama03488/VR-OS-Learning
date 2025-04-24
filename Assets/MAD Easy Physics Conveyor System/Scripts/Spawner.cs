using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
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
        void Start()
        {
            InvokeRepeating("SpawnBox", 2.0f, 3f);
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
    }
}