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
    private PlayableDirector timeline;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        buttons = FindObjectsOfType<ButtonScript>();
    }

    public void StartDialogue(Dialogue dialogue, PlayableDirector tl)
    {
        timeline = tl;
        ButtonsAnimator.SetBool("isOpen", false);
        DialogueAnimator.SetBool("isOpen", true);
        NameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
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
        
        ButtonsAnimator.SetBool("isOpen", true);
    }
}
