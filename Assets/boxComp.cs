using System.Collections;
using UnityEngine;

public class boxComp : MonoBehaviour
{
    public int TargetSlot = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DisableObject()
    {

    }
    IEnumerator destroybox()
    {if(PC_Problem.Instance.Boxes> PC_Problem.Instance.NumBoxes)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            PC_Problem.Instance.DecrementAmount();
            Destroy(this.gameObject);
        }
        else
        {
            
            yield return null;
        }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pit")
        {
            StartCoroutine(destroybox());
        }
    }
}
