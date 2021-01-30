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
       
        string name = "";
        Debug.Log("Count: " + count);
        Debug.Log("NameCount: " + ncount);

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
            Show(sceneOne[count], name, currentScene);

        if(count == sceneOne.Length - 1)
        {
            player.enabled = true;
            active = false;
        }
    }
}
