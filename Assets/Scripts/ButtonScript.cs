using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ButtonScript : MonoBehaviour
{
    public HeadScript Head;
    private RectTransform rectTransform;
    private bool clicked;
    private float speed = 1.0f;
    public GameObject DummyObject;
    //public bool isAbout = false;
    public GameObject GameObjectToSetActive;
    private Transform cameraTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        Head = GameObject.FindGameObjectWithTag("Head").GetComponent<HeadScript>();
        rectTransform = this.GetComponent<RectTransform>();
        PlayableDirector timeline = GetComponent<PlayableDirector>();
        //dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
    }
    
    public void StartSequence()
    {
        ButtonScript[] buttons = FindObjectsOfType<ButtonScript>();
        foreach(ButtonScript button in buttons)
        {
            button.gameObject.SetActive(false);
        }
        GameObjectToSetActive.SetActive(true);

        
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = camera.transform;
        Head.SlowlyLookAt(cameraTransform);
    }
    
}
