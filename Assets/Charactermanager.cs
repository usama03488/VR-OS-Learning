using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Charactermanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int WorkerId = 0;
    public NavMeshAgent agent;
    public Animator animator;
    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }
    public void StartMoving()
    {
        animator.SetBool("StartWalk", true);
        agent.SetDestination(NavMeshTargets.Instance.GetTargetpoint().position);
        
    }
    IEnumerator Disabler()
    {
        yield return new WaitForSeconds(20);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
   
   
}
