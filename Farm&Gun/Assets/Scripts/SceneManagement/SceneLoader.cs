using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private class LoadingMonoBehaviour : MonoBehaviour { } // dummy class for coroutine

    public enum Scene
    {
        MainMenu,
        PauseScreen,
        SampleScene,
        LoadingScene,
        GameOver
    } // names of the scenes to be loaded

    private static AsyncOperation asyncOperation;
    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        }; // setting anonymous method to a delegate

        SceneManager.LoadScene(scene.ToString());
    }

    public static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        asyncOperation = SceneManager.LoadSceneAsync(Scene.LoadingScene.ToString());

        while(!asyncOperation.isDone) 
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(asyncOperation != null)
        {
            return asyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback() // this is triggered after the first update
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
