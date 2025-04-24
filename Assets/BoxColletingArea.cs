using UnityEngine;

public class BoxColletingArea : MonoBehaviour
{
    public PC_Problem problemManager;
    int totalBoxes=0;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
           
            if (totalBoxes >= PC_Problem.Instance.NumBoxes)
            {
                animator.SetBool("isPushButton", true);
            }
            else
            {
                PC_Problem.Instance.IncrementAmount();
            }
        }
    }
}
