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
    public string[] sceneSweep;
    public string[] sceneBucket;

    public string[] sceneFinal;
    Farmer farmer;
    public int activeTask;
    private string CurrentText;
    private bool scrollDone = false;
    private bool active;
    private int currentScene;
    int count = 0;
    int ncount = 1;
    CanvasGroup group;

    public CharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
        next.enabled = false;
        farmer = GameObject.FindObjectOfType<Farmer>();

    }

    private void Update()
    {
        //Debug.Log("activated");
        if (Input.GetKeyDown("e") && scrollDone == false)
        {
            Debug.Log("Skipped");

            skipText();
            scrollDone = true;
        }
        else if (Input.GetKeyDown("e") && scrollDone == true && active == true)
        {
            Debug.Log("Next texted");

            nextText();
        }
        else if (Input.GetKeyDown("e") && scrollDone == true && active == false)
            Close();
    }

    public void Show(string text, string name, int scene, bool oneShot)
    {

        currentScene = scene;
        Debug.Log("CurrentS: " + scene);
        group.alpha = 1;
        CurrentText = text;
        speaker.text = name;
        if (oneShot == false)
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


    private void nextText()
    {
        Debug.Log("Count: " + count);
        Debug.Log("Active: " + active);
        Debug.Log("ActiveTask: " + activeTask);
        Debug.Log("Current Scene: " + currentScene);
        Debug.Log("ScrollDone: " + scrollDone);

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

        if (currentScene == 1)
        {
            count++;

            Debug.Log("Count: " + count);
            Debug.Log("Length: " + sceneOne.Length);

            Show(sceneOne[count], name, currentScene, false);
            if (count == sceneOne.Length - 1)
            {
                Debug.Log("Triggered1");
                player.enabled = true;
                active = false;
            }
        }
        //if (currentScene == 3)
        //{
        //    Debug.Log("TwoCount: " + count);

        //    Debug.Log("TwoLength: " + sceneAxe.Length);
        //    Show(sceneAxe[count], name, currentScene);
        //    if (count == sceneAxe.Length - 1)
        //    {
        //        Debug.Log("Triggered2");

        //        player.enabled = true;
        //        active = false;
        //    }
        //}
        else if (currentScene == 5)
        {
            count++;
            Debug.Log("Count: " + count);

            Show(sceneToilet[count], name, currentScene, false);
            if (count == sceneToilet.Length - 1)
            {
                Debug.Log("Triggered Toilet");
                //farmer.check++;
                activeTask = 1;
                player.enabled = true;
                active = false;

            }
        }
        else if (currentScene == 6)
        {
            count++;
            Debug.Log("Count: " + count);

            Show(sceneSweep[count], name, currentScene, false);
            if (count == sceneSweep.Length - 1)
            {
                Debug.Log("Triggered Sweep");
                activeTask = 2;
                //farmer.check++;
                Debug.Log("ActiveTask: " + activeTask);

                player.enabled = true;
                active = false;
            }
        }

        else if (currentScene == 7)
        {
            count++;
            Debug.Log("Count: " + count);

            Show(sceneBucket[count], name, currentScene, false);
            if (count == sceneBucket.Length - 1)
            {

                Debug.Log("Triggered Bucket");
                activeTask = 3;

                player.enabled = true;
                active = false;
            }
        }
        else if (currentScene == 8)
        {
            count++;
            Debug.Log("Count: " + count);

            Show(sceneEgg[count], name, currentScene, false);
            if (count == sceneEgg.Length - 1)
            {
                Debug.Log("Triggered Egg");
                activeTask = 4;

                player.enabled = true;
                active = false;
            }
        }
        else if (currentScene == 9)
        {
            count++;
            Debug.Log("Triggered Hay");

            Show(sceneHay[count], name, currentScene, false);
            if (count == sceneHay.Length - 1)
            {
                activeTask = 5;

                player.enabled = true;
                active = false;
            }
        }
        else if (currentScene == 10)
        {
            Debug.Log("Triggered Poop");

            count++;

            Show(scenePoop[count], name, currentScene, false);
            if (count == scenePoop.Length - 1)
            {
                activeTask = 6;

                player.enabled = true;
                active = false;
            }
        }


        else if (currentScene == 11)
        {
            count++;
            Debug.Log("Triggered Final");
            Debug.Log("scenfinal" + sceneFinal.Length);
            Show(sceneFinal[count], name, currentScene, false);
            if (count == sceneFinal.Length - 1)
            {
                activeTask = 7;
                player.enabled = true;
                active = false;
            }
        }
        
    }
}
