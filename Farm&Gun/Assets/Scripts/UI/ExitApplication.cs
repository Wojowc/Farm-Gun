using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    /**
     * Method used for exiting application
     */
    public void Exit()
    {
        Debug.Log("Application Exited");
        Application.Quit();
    }
}