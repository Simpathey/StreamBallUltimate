using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    [SerializeField] Transform MarblePos;
    Transform TextPos;
    Vector3 Offset = new Vector3(0, .9f, 0);
    [SerializeField] MarbleObject MyMarbleObject;
    Animator TextAnimator;

    private void Start()
    {
        TextPos = GetComponent<Transform>();
        TextAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        TextPos.position = MarblePos.position + Offset;
    }
    public void DisplayScore()
    {
        MyMarbleObject.TransitionToScoreText();
    }

    public void TriggerAnimation()
    {
        TextAnimator.SetTrigger("showScore");
    }
}
