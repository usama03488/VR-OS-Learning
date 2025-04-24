using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PC_Problem : MonoBehaviour
{
    public int Boxes = 0;
    public int NumBoxes = 0;
    public static PC_Problem Instance;
    public Animator animator;
    public bool isFull=false;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void IncrementAmount()
    {
        if (!isFull)
        {
            Boxes++;
            if (Boxes > NumBoxes)
            {
                animator.SetBool("isPushButton", true);
                isFull = true;
                StartCoroutine(CheckAmount());
            }
        }
     
    }
    public void DecrementAmount()
    {
        Boxes--;
        
            
    }
    IEnumerator CheckAmount()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        animator.SetBool("isPushButton", false);
        yield return new WaitForSeconds(3f);
        animator.SetBool("isPushButton", true);
        yield return new WaitForSeconds(3f);
        if (Boxes < 10)
        {
            animator.SetBool("isPushButton", false);
            isFull = false;
        }
    }

    public int GetAmount()
    {
        return Boxes;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
