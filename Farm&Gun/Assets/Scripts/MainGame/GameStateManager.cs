using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.Rendering;

public class GameStateManager : MonoBehaviour
{
    public const string WIN_STRING = "WIN";
    public const string LOOSE_STRING = "LOST";

    [SerializeField]
    public GameBaseState currentState;

    public GameStateIntro IntroState;
    public GameStateDiceroll DicerollState;
    public GameStateDay DayState;
    public GameStateNight NightState;
    public GameStateMarket MarketState;
    public GameStateEnd EndState;

    public GameObject AnimalGenerator;
    public GameObject MarketManager;
    public GameObject GameOverScreenManager;
    public GameObject BuildingPlacement;
    public GameObject LightingManager;
    public GameObject TerrainGenerator;
    public GameObject DayNightBarCanvas;
    public GameObject IntroCanvas;
    public GameObject OpponentsGenerator;
    public GameObject DiceRollComponents;

    public VolumeProfile Day;
    public VolumeProfile Night;
    public GameObject Postprocessing;

    
    public GameObject Player;
    public GameObject MinimapCamera;

    public NavMeshSurface Surface;

    private void Awake()
    {
        IntroState = gameObject.AddComponent<GameStateIntro>();
        DicerollState = gameObject.AddComponent<GameStateDiceroll>();
        DayState = gameObject.AddComponent<GameStateDay>();
        NightState = gameObject.AddComponent<GameStateNight>();
        MarketState = gameObject.AddComponent<GameStateMarket>();
        EndState = gameObject.AddComponent<GameStateEnd>();
    }

    private void Start()
    {
        currentState = IntroState;
        currentState.EnterState(this);
        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state, params string[] args)
    {
        currentState = state;
        state.EnterState(this, args);
    }
}
