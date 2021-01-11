using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarbleObject : MonoBehaviour
{
    //Instantiate object that can do a task, collide with boundaries, provide locational information for points scoring
    //Ball needs to be tied to a player chat ID
    //Can only have one marble per player, should not instantiate if player has ball in play

    //FOR EVENTS:  -go to appropriate starting location 
    //                    -give instructions on where to go (should have random factor)
    //                    -Despawn Object
    float LongJumpForce;
    [SerializeField] float HighJumpForce = 1.0f;
    Rigidbody2D Rigidbody;
    public string PlayerId;
    public SpriteRenderer GameMarbleSprite;
    public TextMeshPro PlayerName;
    float Speed;
    GameData GameData;
    private string JumpDistance;
    [SerializeField] TextFollow MarbleText;
    public bool IsRolling;
    Commands Commands;

    GameController GameController;
    string GameState;

    void Start()
    {
        IsRolling = true;
        Rigidbody = GetComponent<Rigidbody2D>();
        FreeRotation();
        GameData = FindObjectOfType<GameData>();
        GameController = FindObjectOfType<GameController>();
        GameState = GameController.FindGameState();
        ActivateGameState(GameState);
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
        int percentage = UnityEngine.Random.Range(0, 10001);
        if (percentage == 10000)
        {
            Commands = FindObjectOfType<Commands>();
            Commands.AkaiEasterEgg(PlayerName.text);
        }
        //Force acting on the marble
        float range = 4.8f + (((7.9f / 10000f) * percentage));
        Debug.Log(range);
        float distance = ((100f / 10000f) * percentage);
        JumpDistance = System.String.Format("{0:0.00}", distance);
        int score = Mathf.RoundToInt(distance);
        LongJumpForce = range;
        //transform.position = new Vector3(-13.5f, -4.828952f, 0f);
        Rigidbody.AddForce(new Vector2(LongJumpForce, 0), ForceMode2D.Impulse);
        StartCoroutine(WaitUntilMovementStops(PlayerId, score));

    }
    public void HighJump()
    {
        //How far player jumps
        int percentage = UnityEngine.Random.Range(0, 10000);
        if (percentage == 10000)
        {
            Commands = FindObjectOfType<Commands>();
            Commands.AkaiEasterEgg(PlayerName.text);
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
        Rigidbody.AddForce(new Vector2(0, -1 * (HighJumpForce)), ForceMode2D.Impulse);
        //StartCoroutine(WaitUntilMovementStops(playerID, score));


    }
    IEnumerator WaitUntilMovementStops(string ID, int money)
    {
        do
        {
            Speed = Rigidbody.velocity.magnitude;
            yield return new WaitForSeconds(0.5f);
        }
        while (Speed > 0);
        Debug.Log("MOVEMENTSTOPPED");
        MarbleText.TriggerAnimation();
        GameData.AddMoneyToPlayerID(money, ID);
        IsRolling = false;
    }

    public void LockRotation()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void FreeRotation()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.None;
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
        PlayerName.text += $"\n{JumpDistance}";
        //jumpDistance.ToString();
    }
}
