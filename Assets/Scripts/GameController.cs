using System.Collections; // Don't eat TheBookSnail or you might get rat lungworm :0
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public enum gameState { downtime, cutscene, gametime };
public enum gameMode { longjump, highjump, race };
public class GameController : MonoBehaviour
{
    public DataManager dataManager;
    public gameState currentState;
    public gameMode currentGameMode;
    [SerializeField] TextMeshPro gameStateText;
    JumpManager jumpManager;
    public Shop gameShop;
    [SerializeField] Null nullCharacter;
    [SerializeField] Timer timer;
    [SerializeField] Shop shop;

    //The game has states Downtime, Cutscene, Gametime
    //Gametime can link to different game modes longJump, highJump, race 

    void Start()
    {
        currentGameMode = gameMode.longjump;
        currentState = gameState.downtime;
        jumpManager = FindObjectOfType<JumpManager>();
        UpdateGameStateText();
    }

    private void UpdateGameStateText()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case gameState.downtime:
                gameStateText.text = "Down Time";
                break;
            case gameState.cutscene:
                gameStateText.text = "Cut Scene";
                break;
            case gameState.gametime:
                gameStateText.text = "Game Time";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpManager.DestroyMarbles();
        }
    }

    public void TriggerCutscene()
    {
        currentState = gameState.cutscene;
        gameShop.gameObject.SetActive(false);
        nullCharacter.NullStartCutScene();
        UpdateGameStateText();
    }
    public void TriggerGame()
    {
        currentState = gameState.gametime;
        UpdateGameStateText();
        timer.ResetGameTimer();
    }
    public void TriggerDowntime()
    {
        shop.ResetShop();
        currentState = gameState.downtime;
        UpdateGameStateText();
        timer.ResetDowntimeTimer();
        StartCoroutine(jumpManager.DestroyMarbles());
        nullCharacter.HideCharacter();
        gameShop.gameObject.SetActive(true);
    }
    public string FindGameState()
    {
        Debug.Log(currentGameMode);
        switch (currentGameMode)
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
