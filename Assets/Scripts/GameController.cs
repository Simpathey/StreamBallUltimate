using Newtonsoft.Json;
using System.Collections; // Don't eat TheBookSnail or you might get rat lungworm :0
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public DataManager DataManager;
    public GameState CurrentState;
    public GameMode CurrentGameMode;
    [SerializeField] TextMeshPro GameStateText;
    JumpManager JumpManager;
    public Shop GameShop;
    [SerializeField] Null NullCharacter;
    [SerializeField] Timer Timer;
    [SerializeField] Shop Shop;

    //The game has states Downtime, Cutscene, Gametime
    //Gametime can link to different game modes longJump, highJump, race 

    void Start()
    {
        CurrentGameMode = GameMode.LongJump;
        CurrentState = GameState.DownTime;
        JumpManager = FindObjectOfType<JumpManager>();
        UpdateGameStateText();
    }

    private void UpdateGameStateText()
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case GameState.DownTime:
                GameStateText.text = "Down Time";
                break;
            case GameState.Cutscene:
                GameStateText.text = "Cut Scene";
                break;
            case GameState.GameTime:
                GameStateText.text = "Game Time";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            JumpManager.DestroyMarbles();
        }
    }

    public void TriggerCutscene()
    {
        CurrentState = GameState.Cutscene;
        GameShop.gameObject.SetActive(false);
        NullCharacter.NullStartCutScene();
        UpdateGameStateText();
    }
    public void TriggerGame()
    {
        CurrentState = GameState.GameTime;
        UpdateGameStateText();
        Timer.ResetGameTimer();
    }
    public void TriggerDowntime()
    {
        Shop.ResetShop();
        CurrentState = GameState.DownTime;
        UpdateGameStateText();
        Timer.ResetDowntimeTimer();
        StartCoroutine(JumpManager.DestroyMarbles());
        NullCharacter.HideCharacter();
        GameShop.gameObject.SetActive(true);
    }
    public string FindGameState()
    {
        Debug.Log(CurrentGameMode);
        switch (CurrentGameMode)
        {
            case GameMode.LongJump:
                return "Long Jump";
            case GameMode.HighJump:
                Debug.Log("Launching High Jump");
                return "High Jump";
            case GameMode.Race:
                return "Race";
            default:
                return "YOUR MOM";
        }
    }
}
