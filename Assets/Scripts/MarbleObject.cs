using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MarbleObject : MonoBehaviour
{
    //Instantiate object that can do a task, collide with boundaries, provide locational information for points scoring
    //Ball needs to be tied to a player chat ID
    //Can only have one marble per player, should not instantiate if player has ball in play

   //FOR EVENTS:  -go to appropriate starting location 
   //                    -give instructions on where to go (should have random factor)
   //                    -Despawn Object
    float longJumpForce;
    [SerializeField] float highJumpForce = 1.0f;
    Rigidbody2D rb;
    public string playerID; 
    public SpriteRenderer gameMarbleSprite; 
    public TextMeshPro playerName;
    float speed;
    GameData gameData;
    private string jumpDistance;
    [SerializeField] TextFollow marbleText;
    public bool isrolling;
    Commands commands;

    GameController gameController;
    string gameState;

    void Start()
    {
        isrolling = true;
        rb = GetComponent<Rigidbody2D>();
        FreeRotation();
        gameData = FindObjectOfType<GameData>();
        gameController = FindObjectOfType<GameController>();
        gameState = gameController.FindGameState();
        ActivateGameState(gameState);
    }

    private void ActivateGameState(string gameState)
    {
        switch (gameState)
        {
            case "Long Jump":
                LongJump();
                break;
            case "High Jump":
                HighJump();
                break;
            default:
                break;
        }
    }

    public void LongJump()
    {
        //How far player jumps
        int percentage = UnityEngine.Random.Range(0, 10000);
        if (percentage == 10000)
        {
            commands = FindObjectOfType<Commands>();
            commands.AkaiEasterEgg(playerName.text);
        }
        //Force acting on the marble
        float range = 4.8f + (((7.9f / 10000f) * percentage));
        Debug.Log(range);
        float distance = ((100f / 10000f) * percentage);
        jumpDistance = System.String.Format("{0:0.00}", distance);
        int score = Mathf.RoundToInt(distance);
        longJumpForce = range;
        //transform.position = new Vector3(-13.5f, -4.828952f, 0f);
        rb.AddForce(new Vector2(longJumpForce, 0), ForceMode2D.Impulse);
        StartCoroutine(WaitUntilMovementStops(playerID,score));

    }
    public void HighJump()
    {
        //How far player jumps
        int percentage = UnityEngine.Random.Range(0, 10000);
        if (percentage == 10000)
        {
            commands = FindObjectOfType<Commands>();
            commands.AkaiEasterEgg(playerName.text);
        }
        //Force acting on the marble
        /*
        float range = 4.8f + (((7.9f / 10000f) * percentage));
        Debug.Log(range);
        float distance = ((100f / 10000f) * percentage);
        jumpDistance = System.String.Format("{0:0.00}", distance);
        int score = Mathf.RoundToInt(distance);
        longJumpForce = range;
        //transform.position = new Vector3(-13.5f, -4.828952f, 0f);
        */
        rb.AddForce(new Vector2(0, -1*(highJumpForce)), ForceMode2D.Impulse);
        //StartCoroutine(WaitUntilMovementStops(playerID, score));
        

    }
    IEnumerator WaitUntilMovementStops(string ID, int money)
    {
        do
        {
            speed = rb.velocity.magnitude;
            yield return new WaitForSeconds(0.5f);
        }
        while (speed > 0);
        Debug.Log("MOVEMENTSTOPPED");
        marbleText.TriggerAnimation();
        gameData.AddMoneyToPlayerID(money, ID);
        isrolling = false;
    }

    public void LockRotation()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void FreeRotation()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }
    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        GameObject otherGameObject = otherCollider.gameObject;
        if (otherGameObject.layer == 9)
        {
            LockRotation();
        } 
    }
    public void TransitionToScoreText()
    {
        playerName.text += $"\n{jumpDistance}";
            //jumpDistance.ToString();
    }
}
