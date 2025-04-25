using TMPro;
using UnityEngine;

public class PC_LOCK : MonoBehaviour
{
    public Transform Target1;
    public Transform Target2;
    public Npc npc1;
    public Npc npc2;
    public TMP_Text point1_text;
    public TMP_Text point2_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = Random.Range(1, 2);
        if (i == 1)
        {
            npc1.MoveTowards(Target1,point1_text);
            npc2.MoveTowards(Target2, point2_text);
        }
        else
        {
            npc1.MoveTowards(Target2, point2_text);
            npc2.MoveTowards(Target1, point1_text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
