using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerLoader : MonoBehaviour
{
    public void LoadSingleplayer()
    {
        Debug.Log("Loading Singleplayer");
        SceneLoader.Load(SceneLoader.Scene.SampleScene); // TODO: Replace with main game scene
    }
}
