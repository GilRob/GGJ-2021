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
    BoxCollider trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && check == 1 && done == false)
        {
            Dialogue.Show(sceneOne[0], "Farmer", 1);
            player.enabled = false;
            done = true;
        }
        if (other.tag == "Player" && check == 2)
        {
            Dialogue.Show("I ain't got all day boy", "Farmer", 2);
        }
        //if (other.tag == "Player" && check == 3)
        //{
        //    Dialogue.Show("Need you to do a few more things", "Farmer", 3);
        //}
        if (other.tag == "Player" && check == 4)
        {
            Dialogue.Show("My patience is runnin' thin.", "Farmer", 4);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && check == 1)
        {
            Dialogue.Close();
            check++;
            done = false;

        }
        if (other.tag == "Player" && check == 2)
        {
            Dialogue.Close();
        }
        if (other.tag == "Player" && check == 3)
        {
            Dialogue.Close();
            check++;
        }
    }
}
