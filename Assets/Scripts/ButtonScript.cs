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
    private Transform dummyTransform;
    //public bool isAbout = false;
    public GameObject GameObjectToSetActive;
    
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

        if(dummyTransform!=null)
            Destroy(dummyTransform.parent);
        //Vector3 buttonPos = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, -1000);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 cameraPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        dummyTransform = Instantiate(DummyObject, cameraPos, Quaternion.identity).transform;
        Head.SlowlyLookAt(dummyTransform);
    }
    
}
