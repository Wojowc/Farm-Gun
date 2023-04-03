using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void Exit() // Method used for exiting application
    {
        Debug.Log("Exiting application");
        Application.Quit();
    }
}