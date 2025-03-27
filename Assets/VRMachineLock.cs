using UnityEngine;
using UnityEngine.UI;

public class VRMachineLock : MonoBehaviour
{
    public Button lockButton;
    public Text lockStatusText;
    public GameObject Machine_Controls; // Machine object to enable/disable interaction

    private bool isLocked = false;

    void Start()
    {
       // lockButton.onClick.AddListener(ToggleLock);
      
    }

    public void ToggleLock()
    {
        if (!isLocked)
        {
            LockMachine();
        }
        else
        {
            UnlockMachine();
        }
        UpdateUI();
    }

    void LockMachine()
    {
        isLocked = true;
        lockStatusText.text = "Locked";
        Machine_Controls.SetActive(true); // Enable machine usage
    }

    void UnlockMachine()
    {
        isLocked = false;
        lockStatusText.text = "Available";
       Machine_Controls.SetActive(false); // Disable machine usage
    }

    void UpdateUI()
    {
        lockStatusText.text = isLocked ? "Locked" : "Available";
    }
}
