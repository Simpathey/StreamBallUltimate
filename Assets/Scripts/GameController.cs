using Newtonsoft.Json;
using System.Collections; // Don't eat TheBookSnail or you might get rat lungworm :0
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum gameState { downtime, cutscene, gametime };
public enum gameMode { longjump, highjump, race };
public class GameController : MonoBehaviour
{
    public DataManager DataManager;
    public gameState CurrentState;
    public gameMode CurrentGameMode;
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
        CurrentGameMode = gameMode.longjump;
        CurrentState = gameState.downtime;
        JumpManager = FindObjectOfType<JumpManager>();
        UpdateGameStateText();
    }

    private void UpdateGameStateText()
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case gameState.downtime:
                GameStateText.text = "Down Time";
                break;
            case gameState.cutscene:
                GameStateText.text = "Cut Scene";
                break;
            case gameState.gametime:
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
        CurrentState = gameState.cutscene;
        GameShop.gameObject.SetActive(false);
        NullCharacter.NullStartCutScene();
        UpdateGameStateText();
    }
    public void TriggerGame()
    {
        CurrentState = gameState.gametime;
        UpdateGameStateText();
        Timer.ResetGameTimer();
    }
    public void TriggerDowntime()
    {
        Shop.ResetShop();
        CurrentState = gameState.downtime;
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
            case gameMode.longjump:
                return "Long Jump";
            case gameMode.highjump:
                Debug.Log("Launching High Jump");
                return "High Jump";
            case gameMode.race:
                return "Race";
            default:
                return "YOUR MOM";
        }
    }
}
