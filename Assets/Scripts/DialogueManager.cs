using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class DialogueManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public Animator DialogueAnimator;
    public Animator ButtonsAnimator;
    Queue<string> sentences;
    ButtonScript[] buttons;
    public GameObject DialogueBox;
    private PlayableDirector timeline;
    private float timeStarted;
    [SerializeField]private float timeElapsed;
    
    // Start is called before the first frame update
    void Start()
    {
        timeline = gameObject.GetComponent<PlayableDirector>();
        timeline.Pause();
        sentences = new Queue<string>();
        buttons = FindObjectsOfType<ButtonScript>();
    }
    private void Update()
    {
        timeElapsed = Time.time - timeStarted;
        if (timeElapsed > 3)
        {
            timeline.Pause();
            timeStarted = 0;
            timeElapsed = 0;
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //timeline = tl;
        ButtonsAnimator.SetBool("isOpen", false);
        DialogueAnimator.SetBool("isOpen", true);
        NameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        timeStarted = Time.time;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        timeline.Play();
        timeStarted = Time.time;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        

        string sentence = sentences.Dequeue();

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
        DialogueAnimator.SetBool("isOpen", false);
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        DialogueBox.SetActive(false);
        
        ButtonsAnimator.SetBool("isOpen", true);
    }
}
