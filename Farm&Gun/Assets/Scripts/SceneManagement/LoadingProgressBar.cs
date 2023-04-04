using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    private Slider loadingBarSlider; // use it after adding the loading bar image to the scene

    private void Awake()
    {
        loadingBarSlider = transform.GetComponent<Slider>();
    }

    private void Update()
    {
        loadingBarSlider.value  = SceneLoader.GetLoadingProgress();
    }
}