using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshTargets : MonoBehaviour
{
    public static NavMeshTargets Instance;
    public List<Transform> targetPoints;
    private void Awake()
    {
        
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public Transform GetTargetpoint()
    {
        int i= Random.Range(0, targetPoints.Count);
        return targetPoints[i];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
