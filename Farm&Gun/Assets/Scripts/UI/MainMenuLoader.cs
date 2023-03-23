using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    /**
     * Method used for loading Main Menu
     */
    public void loadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
