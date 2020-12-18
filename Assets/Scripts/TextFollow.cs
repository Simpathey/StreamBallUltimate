using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    [SerializeField] Transform marblePos;
    Transform textPos;
    Vector3 offset = new Vector3(0, .9f, 0);
    [SerializeField] MarbleObject myMarbleObject;
    Animator textAnimator;

    private void Start()
    {
        textPos = GetComponent<Transform>();
        textAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        textPos.position = marblePos.position + offset;
    }
    public void DisplayScore()
    {
        myMarbleObject.TransitionToScoreText();
    }

    public void TriggerAnimation()
    {
        textAnimator.SetTrigger("showScore");
    }
}
