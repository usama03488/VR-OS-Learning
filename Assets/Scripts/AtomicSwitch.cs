using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using BNG;
public class AtomicSwitch : MonoBehaviour
{
    private bool isLocked = false;
    public GameObject indicatorLight; // Assign a red/green light in Unity
    public Grabbable Lever_grabbable;
    public Lever levercomp;
    public Light RedLight;
    public Light GreenLight;
    public GameObject ParticleEffect;
    private void Start()
    {
        SetIndicator(false); // Set initial state to available
    }
    public void OnValueChange(float input)
    {
        if (input < 20f && input>0)
        {
            
            Debug.Log($"input{input}");
            TryToggleSwitch();
        }
        
    }
    public void TryToggleSwitch()
    {
       
        if (!isLocked)
        {
            CancelInvoke(nameof(ResetLock));
            Invoke(nameof(ResetLock), 4f); // Lock for 3 seconds
            isLocked = true;
            Lever_grabbable.enabled = false;
            RedLight.enabled = false;
            GreenLight.enabled = true;
            ParticleEffect.SetActive(true);
            // SetIndicator(true);
            Debug.Log("Access granted! Lock engaged.");
         
            // Simulate a process duration before unlocking
            
           
        }
        else
        {
            Debug.Log("Access denied! Already in use.");
           
            // Provide haptic feedback if using XR controllers
        }
    }

    private void ResetLock()
    {
        isLocked = false;
        Lever_grabbable.enabled = true;
        levercomp.SetLeverAngle(0);
        //SetIndicator(false);
        RedLight.enabled = true;
        GreenLight.enabled = false;
        ParticleEffect.SetActive(false);
        Debug.Log("Lock released! Available for use.");
    }

    private void SetIndicator(bool locked)
    {
        //if (indicatorLight)
        //    indicatorLight.GetComponent<Renderer>().material.color = locked ? Color.red : Color.green;
    }
}
