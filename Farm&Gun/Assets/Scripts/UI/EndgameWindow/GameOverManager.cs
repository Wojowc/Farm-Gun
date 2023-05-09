using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private MobGenerator animalSpawningManager;
    [SerializeField]
    private LightingManager lightingManager;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private Image winningImage;

    [SerializeField]
    private Sprite gameWonImageSprite;
    [SerializeField]
    private Sprite gameLostImageSprite;
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI daysAmountText;
    [SerializeField]
    private TextMeshProUGUI ducksAmountText;
    [SerializeField]
    private TextMeshProUGUI pigsAmountText;
    [SerializeField]
    private TextMeshProUGUI chickensAmountText;
    [SerializeField]
    private TextMeshProUGUI sheepAmountText;
    [SerializeField]
    private TextMeshProUGUI cowsAmountText;

    public void GameLost() // when player is dead it will be called
    {
        gameOverCanvas.SetActive(true);
        winningImage.sprite = gameLostImageSprite;
        gameOverText.text = "YOU LOST";
        SetAmountTexts();
    }

    public void GameWon()
    {
        gameOverCanvas.SetActive(true);
        winningImage.sprite = gameWonImageSprite;
        gameOverText.text = "YOU WON";
        SetAmountTexts();
    }

    private void SetAmountTexts()
    {
        var animalCount = animalSpawningManager.GetAnimalCount();
        daysAmountText.text = lightingManager.DaysCount.ToString();
        chickensAmountText.text = animalCount.ChickensAmount.ToString();
        ducksAmountText.text = animalCount.DucksAmount.ToString();
        sheepAmountText.text = animalCount.SheepAmount.ToString();
        pigsAmountText.text = animalCount.PigsAmount.ToString();
        cowsAmountText.text = animalCount.CowsAmount.ToString();
    }

    private void OnContinueClick()
    {
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        gameOverCanvas.SetActive(false);
    }
}
