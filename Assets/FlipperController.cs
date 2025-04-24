using System.Collections;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public Transform flipper; // Assign in Inspector
    public float rotationSpeed = 180f; // degrees per second

    private bool isRotating = false;
    private bool flipped = false; // false = +30, true = -30

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "box")
        {
            Debug.Log("Box triggered");
            TriggerFlip();
        }
    }
    public void TriggerFlip()
    {
        if (!isRotating)
        {
            float targetY = flipped ? 30f : -30f;
            StartCoroutine(RotateToY(targetY));
            flipped = !flipped; // toggle direction
        }
    }

    private IEnumerator RotateToY(float targetY)
    {
        isRotating = true;

        Quaternion startRotation = flipper.rotation;
        Quaternion endRotation = Quaternion.Euler(flipper.eulerAngles.x, targetY, flipper.eulerAngles.z);

        float t = 0f;
        float duration = Quaternion.Angle(startRotation, endRotation) / rotationSpeed;

        while (t < duration)
        {
            t += Time.deltaTime;
            flipper.rotation = Quaternion.Slerp(startRotation, endRotation, t / duration);
            yield return null;
        }

        flipper.rotation = endRotation; // Snap exactly to target
        isRotating = false;
    }
}
