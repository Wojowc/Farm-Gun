using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerLoader : MonoBehaviour
{
    /**
     * Method used for loading singleplayer Game Scene
     */
    public void loadSingleplayer()
    {
        Debug.Log("Loading Singleplayer");
        SceneLoader.Load(SceneLoader.Scene.SampleScene);
    }
}
