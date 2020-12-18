using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Null : MonoBehaviour
{
    //NULL is a charasmatic Lady bug who is the master of ceremony for the stream marble games!
    [SerializeField] GameController gameController;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void NullStartCutScene()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartingDialogue());
    }

    IEnumerator StartingDialogue()
    {
        yield return new WaitForSeconds(10);
        gameController.TriggerGame();
    }
    public void HideCharacter()
    {
        gameObject.SetActive(false);
    }
}
