﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class DialogueManager : MonoBehaviour
{
    //FIX DIALOGUE STEPPING!!!!!!!!!!!!!!!!!

    public Text NameText;
    public Text DialogueText;
    public Animator DialogueAnimator;
    public Animator ButtonsAnimator;
    Queue<string> sentences;
    ButtonScript[] buttons;
    public GameObject DialogueBox;
    private PlayableDirector timeline;
    private float timeStarted;
    private float stepTime = 3;
    bool dialogueStarted;
    public GameObject Head;
    public int dialogueIndex;
    [SerializeField]private float timeElapsed;
    Queue<GameObject> gameObjectsToShow;
    Queue<GameObject> lookAtQueue;
    public GameObject emptyObj;
    

    // Start is called before the first frame update
    void Start()
    {
        timeline = gameObject.GetComponent<PlayableDirector>();
        timeline.Pause();
        sentences = new Queue<string>();
        gameObjectsToShow = new Queue<GameObject>();
        lookAtQueue = new Queue<GameObject>();
        buttons = FindObjectsOfType<ButtonScript>();

        Head = GameObject.FindGameObjectWithTag("Head");
        
    }
    private void Update()
    {
        if(dialogueStarted)
        {
            timeElapsed = Time.time - timeStarted;
        }
        
        if (timeElapsed > stepTime)
        {
            timeline.Pause();
            timeStarted = 0;
            timeElapsed = 0;
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //timeline = tl;
        dialogueStarted = true;
        ButtonsAnimator.SetBool("isOpen", false);
        DialogueAnimator.SetBool("isOpen", true);
        NameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
       foreach(GameObject go in dialogue.objectArray)
        {
            print(go);
            gameObjectsToShow.Enqueue(go);
        }
       foreach(GameObject lookAtObject in dialogue.whatToLookAt)
        {
            lookAtQueue.Enqueue(lookAtObject);
        }
        timeStarted = Time.time;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        dialogueIndex++;
        timeline.time += stepTime - timeElapsed;
        timeline.Play();
        timeStarted = Time.time;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        GameObject obj = gameObjectsToShow.Dequeue();
        GameObject objToLookAt = lookAtQueue.Dequeue();
        obj.SetActive(true);
        if (objToLookAt.tag != "EmptyObject")
        {
            Head.GetComponent<HeadScript>().SlowlyLookAt(objToLookAt.transform);
        }
        else
        {
            Head.GetComponent<HeadScript>().SlowlyLookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {

        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        
        timeElapsed = 0;
        DialogueAnimator.SetBool("isOpen", false);
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        GameObject[] dialogueItems = GameObject.FindGameObjectsWithTag("DialogueItem");
        foreach(GameObject item in dialogueItems)
        {
            item.SetActive(false);
        }
        DialogueBox.SetActive(false);
        
        ButtonsAnimator.SetBool("isOpen", true);
        dialogueStarted = false;
        Head.GetComponent<HeadScript>().SlowlyLookAt(GameObject.FindGameObjectWithTag("ForwardObject").transform);
    }
}
