using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Image loadingBarImage; // use it after adding the loading bar image to the scene

    private void Awake()
    {
        loadingBarImage = transform.GetComponent<Image>();
    }

    void Update()
    {
        loadingBarImage.fillAmount = SceneLoader.GetLoadingProgress();
    }
}
