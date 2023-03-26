using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    /**
     * Method used for exiting application
     */
    public void Exit()
    {
        Debug.Log("Exiting application");
        Application.Quit();
    }
}