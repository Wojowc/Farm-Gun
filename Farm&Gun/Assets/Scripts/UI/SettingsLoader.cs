using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    public void LoadSettingsMenu() // Method used for loading settings menu
    {
        Debug.Log("Loading Settings");
        SceneLoader.Load(SceneLoader.Scene.Settings);
    }
}
