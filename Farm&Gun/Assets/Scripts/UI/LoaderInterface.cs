using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderInterface : MonoBehaviour
{
    public void LoadSelectedScene(int scene)
    {
        Debug.Log("Loading " + ((SceneLoader.Scene)scene).ToString() + " scene");
        SceneLoader.Load((SceneLoader.Scene)scene);
    }

    //public void LoadSelectedScene2(SceneLoader.Scene scene)
    //{
    //    Debug.Log("Loading " + scene.ToString() + " scene");
    //    SceneLoader.Load(scene);
    //}
}
