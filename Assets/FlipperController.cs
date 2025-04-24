using System.Collections;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public Transform flipper; // Assign in Inspector
    public float rotationSpeed = 180f; // degrees per second

    private bool isRotating = false;

    public void RotateTo(float targetY)
    {
        if (!isRotating)
        {
            StartCoroutine(RotateToLocalY(targetY));
        }
    }

    private IEnumerator RotateToLocalY(float targetY)
    {
        isRotating = true;

        Quaternion startRot = flipper.localRotation;
        Quaternion endRot = Quaternion.Euler(0f, targetY, 0f);

        float angle = Quaternion.Angle(startRot, endRot);
        float duration = angle / rotationSpeed;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            flipper.localRotation = Quaternion.Slerp(startRot, endRot, t / duration);
            yield return null;
        }

        flipper.localRotation = endRot; // Snap exactly to target
        isRotating = false;
    }
}
