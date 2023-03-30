using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    public void loadMainMenu() // Method used for loading main menu
    {
        Debug.Log("Loading Main Menu");
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
