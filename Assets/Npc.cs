using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Transform targetPoint; // The destination
    [SerializeField] private float moveSpeed = 3f;  // Movement speed
    public PC_LOCK lock_manager;
    public TMP_Text text;
    public void MoveTowards(Transform point, TMP_Text tex)
    {
        text=tex;
        targetPoint = point;
    }
    void Update()
    {
        if (targetPoint == null) return;

        // Move NPC toward the target
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Optional: rotate to face the direction of movement
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }
        if(Vector3.Distance(transform.position, targetPoint.position) < 2)
        {
            text.text = "Not Available";
            text.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
