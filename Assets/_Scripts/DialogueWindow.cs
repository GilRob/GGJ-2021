using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{

    public Text text;
    public Text speaker;
    public Image next;
    public string[] sceneOne;
    public string[] sceneAxe;
    public string[] sceneToilet;
    public string[] scenePoop;
    public string[] sceneEgg;
    public string[] sceneHay;
    public string[] sceneFinal;

    private string CurrentText;
    private bool scrollDone = false;
    private bool active;
    private int currentScene;
    CanvasGroup group;

    public CharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
        next.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") && scrollDone == false)
        {
            skipText();
            scrollDone = true;
        }
        else if (Input.GetKeyDown("e") && scrollDone == true && active == true)
        {
            nextText();
        }
    }

    public void Show(string text, string name, int scene)
    {
        currentScene = scene;
        group.alpha = 1;
        CurrentText = text;
        speaker.text = name;
        active = true;
        StartCoroutine(DisplayText());
    }

    public void Close()
    {
        StopAllCoroutines();
        active = false;
        group.alpha = 0;
        count = 0;
        ncount = 1;
        Debug.Log("Closed");
    }

    public void skipText()
    {
        StopAllCoroutines();
        text.text = CurrentText;
        next.enabled = true;
    }

    private IEnumerator DisplayText()
    {
        text.text = "";
        next.enabled = false;
        scrollDone = false;
        foreach (char c in CurrentText.ToCharArray())
        {
            text.text += c;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        next.enabled = true;
        scrollDone = true;

    }

    int count = 0;
    int ncount = 1;
    private void nextText()
    {
        //Debug.Log("Count" + count);

        string name = "";
        //Debug.Log("Count: " + count);
        //Debug.Log("NameCount: " + ncount);

        if (ncount == 0)
        {
            name = "Farmer";
            ncount++;
        }
        else if (ncount == 1)
        {
            name = "Player";
            ncount--;
        }

        count++;
        if (currentScene == 1)
        {
            Debug.Log("Count: " + count);
            Debug.Log("Length: " + sceneOne.Length);

            Show(sceneOne[count], name, currentScene);
            if (count == sceneOne.Length - 1)
            {
                Debug.Log("Triggered1");
                player.enabled = true;
                active = false;
            }
        }
        if (currentScene == 3)
        {
            Debug.Log("TwoCount: " + count);

            Debug.Log("TwoLength: " + sceneAxe.Length);
            Show(sceneAxe[count], name, currentScene);
            if (count == sceneAxe.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
        }
        if (currentScene == 5)
        { 
            Show(sceneToilet[count], name, currentScene);
            if (count == sceneToilet.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
    }
      if (currentScene == 6)
        { 
            Show(scenePoop[count], name, currentScene);
            if (count == scenePoop.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
        }  
        if (currentScene == 7)
        { 
            Show(sceneEgg[count], name, currentScene);
            if (count == sceneEgg.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
        }
      if (currentScene == 8)
        { 
            Show(sceneHay[count], name, currentScene);
            if (count == sceneHay.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
        }
        if (currentScene == 9)
        {
            Show(sceneFinal[count], name, currentScene);
            if (count == sceneHay.Length - 1)
            {
                Debug.Log("Triggered2");

                player.enabled = true;
                active = false;
            }
        }
    }
}
