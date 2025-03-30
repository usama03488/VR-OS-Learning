using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
