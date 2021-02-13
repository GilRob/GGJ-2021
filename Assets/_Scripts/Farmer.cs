using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    //public string DialogueText;
    public string[] sceneOne;
    public DialogueWindow Dialogue;
    public CharacterController player;
    public int check = 1;
    public bool done = false;
    public bool inside = false;
    BoxCollider trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log("Check: " + check);
        Debug.Log("Done: " + done);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            inside = true;
        if (other.tag == "Player" && check == 1 && done == false)
        {
            Dialogue.Show(sceneOne[0], "Farmer", 1, false);
            player.enabled = false;
            done = true;
        }
        if (other.tag == "Player" && check == 2)
        {
            Dialogue.Show("I ain't got all day boy", "Farmer", 2, true);
        }
        if (other.tag == "Player" && check == 3 && done == false)
        {
            Dialogue.Show("My patience is runnin' thin.", "Farmer", 4, true);
        }
        if (other.tag == "Player" && check == 4 && done == false)
        {
            Dialogue.Show("Nice, one more thing", "Farmer", Random.Range(7, 9), false);
            player.enabled = false;
            done = true;
        }
        if (other.tag == "Player" && check == 5)
        {
            Dialogue.Show("...", "Farmer", 4, true);
        }
        if (other.tag == "Player" && check == 6 && done == false)
        {
            Dialogue.Show("Nice, one more thing", "Farmer", Random.Range(9, 11), false);
            player.enabled = false;
            done = true;
        }
        if (other.tag == "Player" && check == 7)
        {
            Dialogue.Show("...", "Farmer", 4, true);
        }
        if (other.tag == "Player" && check == 8 && done == false)
        {
            Dialogue.Show("All done? I need one more thing...", "Farmer", 11, false);
            player.enabled = false;
            done = true;
        }
        if (other.tag == "Player" && check == 9)
        {
            Dialogue.Show("It's ok if you decide to leave...", "Farmer", 4, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inside = false;
        if (other.tag == "Player" && check == 1)
        {
            Dialogue.Close();
            check++;
            done = false;

        }
        else if (other.tag == "Player" && check == 2)
        {
            Dialogue.Close();
        }
        else if (other.tag == "Player" && check == 3)
        {
            Dialogue.Close();
            done = false;

        }
        else if (other.tag == "Player" && check == 4)
        {
            Dialogue.Close();
            check++;
            done = false;
        }
        else if (other.tag == "Player" && check == 5)
        {
            Dialogue.Close();
            done = false;

        }
        else if (other.tag == "Player" && check == 6)
        {
            Dialogue.Close();
            check++;
            done = false;
        }
        else if (other.tag == "Player" && check == 7)
        {
            Dialogue.Close();
            done = false;

        }
        else if (other.tag == "Player" && check == 8)
        {
            Dialogue.Close();
            check++;
            done = false;
        }
        else if (other.tag == "Player" && check == 9)
        {
            Dialogue.Close();
            done = false;

        }
        else if (other.tag == "Player" && check == 10)
        {
            Dialogue.Close();
            done = false;

        }
    }
}
