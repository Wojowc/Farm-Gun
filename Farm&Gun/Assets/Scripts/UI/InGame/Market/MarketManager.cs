using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AnimalType = MobGenerator.AnimalType;

public class MarketManager : MonoBehaviour
{
    #region Inventory setters
    [SerializeField]
    private MobGenerator animalsManager;
    [SerializeField]
    private BuildingsManager buildingsInventoryManager;
    [SerializeField]
    private PlayerAttack playerAttack; 

    private Dictionary<AnimalType, int> animalsAmounts = new();
    private Dictionary<BuildingType, int> buildingsAmounts = new();

    #endregion


    #region Current inventory

    #region Animals

    [Space()]
    [SerializeField]
    private TextMeshProUGUI chickenAmountText;
    [SerializeField]
    private TextMeshProUGUI ducksAmountText;
    [SerializeField]
    private TextMeshProUGUI sheepAmountText;
    [SerializeField]
    private TextMeshProUGUI pigsAmountText;
    [SerializeField]
    private TextMeshProUGUI cowsAmountText;

    #endregion

    #region Turrets/ammo

    [SerializeField]
    private TextMeshProUGUI turretsDucksAmountText;
    [SerializeField]
    private TextMeshProUGUI turretsPigsAmountText;
    [SerializeField]
    private TextMeshProUGUI fencesAmountText;
    [SerializeField]
    private TextMeshProUGUI ammoAmountText;

    #endregion

    private void UpdateInventoryDisplay()
    {
        animalsAmounts = animalsManager.GetAnimalsAmounts();
        buildingsAmounts = buildingsInventoryManager.GetBuildingsAmounts();
        chickenAmountText.text = animalsAmounts.GetValueOrDefault(AnimalType.Chicken).ToString();
        ducksAmountText.text = animalsAmounts.GetValueOrDefault(AnimalType.Duck).ToString(); 
        sheepAmountText.text = animalsAmounts.GetValueOrDefault(AnimalType.Sheep).ToString(); 
        pigsAmountText.text = animalsAmounts.GetValueOrDefault(AnimalType.Pig).ToString(); 
        cowsAmountText.text = animalsAmounts.GetValueOrDefault(AnimalType.Cow).ToString();
        turretsDucksAmountText.text = buildingsAmounts.GetValueOrDefault(BuildingType.Ducks).ToString();
        turretsPigsAmountText.text = buildingsAmounts.GetValueOrDefault(BuildingType.Pig).ToString();
        fencesAmountText.text = buildingsAmounts.GetValueOrDefault(BuildingType.Fence).ToString();
        ammoAmountText.text = playerAttack.ammo.ToString();
    }

    #endregion Current inventory

    #region Trade values

    #region Animals

    [Space()]
    [SerializeField]
    private TextMeshProUGUI chickenToDuckText;
    [SerializeField]
    private int chickenToDuckRatio = 6;

    [SerializeField]
    private TextMeshProUGUI duckToSheepText;
    [SerializeField]
    private int duckToSheepRatio = 6;

    [SerializeField]
    private TextMeshProUGUI sheepToPigText;
    [SerializeField]
    private int sheepToPigRatio = 6;

    [SerializeField]
    private TextMeshProUGUI pigToCowText;
    [SerializeField]
    private int pigToCowRatio = 6;

    #endregion Animals

    #region Turrets

    [SerializeField]
    private TextMeshProUGUI turretsDucksCostText;
    [SerializeField]
    private int turretsDucksCostInChickens = 6;
    [SerializeField]
    private TextMeshProUGUI turretsDucksTradedAmountText;
    [SerializeField]
    private int turretsDucksAmount = 1;

    [SerializeField]
    private TextMeshProUGUI turretsPigsCostText;
    [SerializeField]
    private int turretsPigsCostInChickens = 6;
    [SerializeField]
    private TextMeshProUGUI turretsPigsTradedAmountText;
    [SerializeField]
    private int turretsPigsAmount = 1;

    [SerializeField]
    private TextMeshProUGUI fencesCostText;
    [SerializeField]
    private int fencesCostInChickens = 2;
    [SerializeField]
    private TextMeshProUGUI fencesTradedAmountText;
    [SerializeField]
    private int fencesAmount = 1;

    [SerializeField]
    private TextMeshProUGUI ammoCostText;
    [SerializeField]
    private int ammoCostInChickens = 2;
    [SerializeField]
    private TextMeshProUGUI ammoTradedAmountText;
    [SerializeField]
    private int ammoAmount = 10;

    #endregion Turrets

    private int[] tradeValues = Array.Empty<int>();

    #endregion

    #region Trade logic

    #region Buttons

    [Space()]
    [SerializeField]
    private Button chickenDuckTradeButton;
    [SerializeField]
    private Button duckSheepTradeButton;
    [SerializeField]
    private Button sheepPigTradeButton;
    [SerializeField]
    private Button pigCowTradeButton;

    [SerializeField]
    private Button turretDucksTradeButton;
    [SerializeField]
    private Button turretPigTradeButton;
    [SerializeField]
    private Button fenceTradeButton;
    [SerializeField]
    private Button ammoTradeButton;

    private Button[] tradeButtons = Array.Empty<Button>();

    #endregion

    #region Button methods

    private void UpdateAfterTrade()
    {
        UpdateInventoryDisplay();
        CheckButtonsInteractability();
    }

    // will have to unify trades so they will be more scalable and changeable, maybe tradeType enum or something like this
    #region Trades

    private void TradeForDuck()
    {
        animalsManager.UpdateAnimalAmount(AnimalType.Duck, animalsAmounts.GetValueOrDefault(AnimalType.Duck) + 1);
        animalsManager.UpdateAnimalAmount(AnimalType.Chicken, animalsAmounts.GetValueOrDefault(AnimalType.Chicken) - tradeValues[0]);
        UpdateAfterTrade();
    }

    private void TradeForSheep()
    {
        animalsManager.UpdateAnimalAmount(AnimalType.Sheep, animalsAmounts.GetValueOrDefault(AnimalType.Sheep) + 1);
        animalsManager.UpdateAnimalAmount(AnimalType.Duck, animalsAmounts.GetValueOrDefault(AnimalType.Duck) - tradeValues[1]);
        UpdateAfterTrade();
    }

    private void TradeForPig()
    {
        animalsManager.UpdateAnimalAmount(AnimalType.Pig, animalsAmounts.GetValueOrDefault(AnimalType.Pig) + 1);
        animalsManager.UpdateAnimalAmount(AnimalType.Sheep, animalsAmounts.GetValueOrDefault(AnimalType.Sheep) - tradeValues[2]);
        UpdateAfterTrade();
    }

    private void TradeForCow()
    {
        animalsManager.UpdateAnimalAmount(AnimalType.Cow, animalsAmounts.GetValueOrDefault(AnimalType.Cow) + 1);
        animalsManager.UpdateAnimalAmount(AnimalType.Pig, animalsAmounts.GetValueOrDefault(AnimalType.Pig) - tradeValues[3]);
        UpdateAfterTrade();
    }

    private void TradeForDucksTurret()
    {
        buildingsInventoryManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Ducks) + turretsDucksAmount, BuildingType.Ducks);
        animalsManager.UpdateAnimalAmount(AnimalType.Chicken, animalsAmounts.GetValueOrDefault(AnimalType.Chicken) - tradeValues[4]);
        UpdateAfterTrade();
    }

    private void TradeForPigTurret()
    {
        buildingsInventoryManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Pig) + turretsPigsAmount, BuildingType.Pig);
        animalsManager.UpdateAnimalAmount(AnimalType.Chicken, animalsAmounts.GetValueOrDefault(AnimalType.Chicken) - tradeValues[5]);
        UpdateAfterTrade();
    }

    private void TradeForFences()
    {
        buildingsInventoryManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Fence) + fencesAmount, BuildingType.Fence);
        animalsManager.UpdateAnimalAmount(AnimalType.Chicken, animalsAmounts.GetValueOrDefault(AnimalType.Chicken) - tradeValues[6]);
        UpdateAfterTrade();
    }

    private void TradeForAmmo()
    {
        playerAttack.ammo = playerAttack.ammo + ammoAmount;
        animalsManager.UpdateAnimalAmount(AnimalType.Chicken, animalsAmounts.GetValueOrDefault(AnimalType.Chicken) - tradeValues[7]);
        UpdateAfterTrade();
    }

    #endregion

    private void CheckButtonsInteractability()
    {

        for(int i = 0; i < 4; i++)
        {
            if (animalsAmounts.GetValueOrDefault((AnimalType)i) < tradeValues[i])
            {
                tradeButtons[i].interactable = false;
            }
            else
            {
                tradeButtons[i].interactable = true;
            }
        }

        var amountOfChickens = animalsAmounts.GetValueOrDefault(AnimalType.Chicken);
        for(int i = 4; i < 8; i++)
        {
            if(amountOfChickens < tradeValues[i])
            {
                tradeButtons[i].interactable = false;
            }
            else
            {
                tradeButtons[i].interactable = true;
            }
        }
    }

    private void SetupButtonTradeLists()
    {
        tradeValues = new int[] {
            chickenToDuckRatio,
            duckToSheepRatio,
            sheepToPigRatio,
            pigToCowRatio,
            turretsDucksCostInChickens,
            turretsPigsCostInChickens,
            fencesCostInChickens,
            ammoCostInChickens 
        };

        tradeButtons = new Button[] {
            chickenDuckTradeButton,
            duckSheepTradeButton,
            sheepPigTradeButton,
            pigCowTradeButton,
            turretDucksTradeButton,
            turretPigTradeButton,
            fenceTradeButton, 
            ammoTradeButton
        };
    }

    private void SetupButtonOnClickMethods()
    {
        chickenDuckTradeButton.onClick.AddListener(TradeForDuck);
        duckSheepTradeButton.onClick.AddListener(TradeForSheep);
        sheepPigTradeButton.onClick.AddListener(TradeForPig);
        pigCowTradeButton.onClick.AddListener(TradeForCow);
        turretDucksTradeButton.onClick.AddListener(TradeForDucksTurret);
        turretPigTradeButton.onClick.AddListener(TradeForPigTurret);
        fenceTradeButton.onClick.AddListener(TradeForFences);
        ammoTradeButton.onClick.AddListener(TradeForAmmo);
    }

    #endregion

    private void SetTradeValues()
    {
        chickenToDuckText.text = chickenToDuckRatio.ToString();
        duckToSheepText.text = chickenToDuckRatio.ToString();
        sheepToPigText.text = chickenToDuckRatio.ToString();
        pigToCowText.text = chickenToDuckRatio.ToString();

        turretsDucksCostText.text = turretsDucksCostInChickens.ToString();
        turretsDucksTradedAmountText.text = turretsDucksAmount.ToString();

        turretsPigsCostText.text = turretsPigsCostInChickens.ToString();
        turretsPigsTradedAmountText.text = turretsPigsAmount.ToString();

        fencesCostText.text = fencesCostInChickens.ToString();
        fencesTradedAmountText.text = fencesAmount.ToString();

        ammoCostText.text = ammoCostInChickens.ToString();
        ammoTradedAmountText.text = ammoAmount.ToString();
    }

    #endregion Trade logic

    private void Awake()
    {
        UpdateInventoryDisplay();
        SetupButtonOnClickMethods();
        SetupButtonTradeLists();
        SetTradeValues();
        UpdateInventoryDisplay();
        CheckButtonsInteractability();
    }

    private void OnEnable()
    {
        UpdateInventoryDisplay();
        CheckButtonsInteractability();
    }

}
