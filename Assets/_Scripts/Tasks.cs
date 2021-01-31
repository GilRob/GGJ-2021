using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController player;
    CharacterController pMovement;
    public GameObject Hayroll;
    public GameObject Egg;
    static public bool didHayroll = false;
    bool[] taskList;
    public DialogueWindow dialogues;
    bool gotEggs = false;
    Farmer farmer;
    //toilet variables
    float startTime = 0f;
    float holdTime = 3f;

    float timer = 0f;
    public Material cleanToilet;
    public Material cleanMatt;
    //public int[] dialogues;
    //public int element;
///////////////////////////////////

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        farmer = GameObject.FindObjectOfType<Farmer>();
        pMovement = GameObject.FindObjectOfType<CharacterController>();

        taskList = new bool[7];

        for(int i =0; i < 7;i++)
        {
            taskList[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.currentItem);
        ////////////////////////////////////////////////////////////////////////////////////
        //Bring Axe to Farmer Task
        //Debug.Log(dialogues.activeTask);
        if(player.hitInfo.collider != null)
        {
            if(player.hitInfo.collider.tag == "Farmer")
            {
                if(Input.GetButtonDown("Interact") && player.currentItem.name == "Axe")
                {
                    Debug.Log("Respect Fam");
                    taskList[0] = true;
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                    player.currentItem.name = "Empty";
                    farmer.check++;
                    //element = Random.Range(0, dialogues.Count);
                    farmer.Dialogue.Show("My axe...Need you to do a few more things", "Farmer", Random.Range(5,7));
                    //dialogues.RemoveAt(element);
                    pMovement.enabled = false;
                    farmer.done = true;

                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        //Roll Hayroll to Spot *collision code in chekCollision.cs*
        if (dialogues.activeTask == 5)
        {
            if (Vector3.Distance(this.transform.position, Hayroll.transform.position) <= 3f && taskList[1] == false)
            {
                Hayroll.GetComponent<Rigidbody>().AddForce(this.transform.forward * 0.5f);
            }


            if (didHayroll == true)
            {
                taskList[1] = true;
                Debug.Log("task complete");
                didHayroll = false;
                farmer.check++;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////
        //Bring bucket to Chicken Coop
        if (dialogues.activeTask == 3)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "ChickenCoop")
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "bucket")
                    {
                        Debug.Log("Fed the Chickens");
                        taskList[2] = true;
                        Destroy(player.currentItem);
                        player.currentlyHolding = false;
                        player.currentItem = new GameObject();
                        player.currentItem.name = "Empty";
                        farmer.check++;
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        //Collect Chicken Eggs
        if (dialogues.activeTask == 4)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "ChickenCoop" && taskList[3] == false)
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "Empty")
                    {
                        player.currentItem = Instantiate(Egg, player.holdPosition.position, player.holdPosition.rotation);
                        player.currentlyHolding = true;
                        gotEggs = true;
                    }
                }

                if (player.hitInfo.collider.name == "EggBasket" && taskList[3] == false)
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "Egg(Clone)" && gotEggs == true)
                    {
                        taskList[3] = true;
                        Destroy(player.currentItem);
                        player.currentlyHolding = false;
                        player.currentItem = new GameObject();
                        player.currentItem.name = "Empty";
                        farmer.check++;

                        Debug.Log("Got The Eggs");
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //clean toilet
        if (dialogues.activeTask == 1)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "toilet" && taskList[4] == false)
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "toiletBrush")
                    {
                        startTime = Time.deltaTime;
                    }

                    if (Input.GetButton("Interact") && player.currentItem.name == "toiletBrush")
                    {
                        timer += Time.deltaTime;

                        // Debug.Log(Time.deltaTime);
                        if (timer >= (startTime + holdTime))
                        {
                            taskList[4] = true;
                            timer = 0;
                            player.hitInfo.collider.GetComponent<Renderer>().material = cleanToilet;
                            Destroy(player.currentItem);
                            player.currentlyHolding = false;
                            player.currentItem = new GameObject();
                            player.currentItem.name = "Empty";
                            Debug.Log("Toilet Cleaned");
                            farmer.check++;

                        }
                    }
                    else
                    {
                        timer = 0;
                    }
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        //clean poop in barn
        if (dialogues.activeTask == 6)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "poop" && taskList[5] == false)
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "pitchFork")
                    {
                        startTime = Time.deltaTime;
                    }

                    if (Input.GetButton("Interact") && player.currentItem.name == "pitchFork")
                    {
                        timer += Time.deltaTime;

                        // Debug.Log(Time.deltaTime);
                        if (timer >= (startTime + holdTime))
                        {
                            taskList[5] = true;
                            timer = 0;
                            Destroy(player.hitInfo.collider.gameObject);
                            Destroy(player.currentItem);
                            player.currentlyHolding = false;
                            player.currentItem = new GameObject();
                            player.currentItem.name = "Empty";
                            Debug.Log("Poop Cleaned");
                            farmer.check++;
                        }
                    }
                    else
                    {
                        timer = 0;
                    }
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //sweep the porch
        if (dialogues.activeTask == 2)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "matt" && taskList[6] == false)
                {
                    if (Input.GetButtonDown("Interact") && player.currentItem.name == "broom")
                    {
                        startTime = Time.deltaTime;
                    }

                    if (Input.GetButton("Interact") && player.currentItem.name == "broom")
                    {
                        timer += Time.deltaTime;

                        // Debug.Log(Time.deltaTime);
                        if (timer >= (startTime + holdTime))
                        {
                            taskList[6] = true;
                            timer = 0;
                            player.hitInfo.collider.GetComponent<Renderer>().material = cleanMatt;
                            Destroy(player.currentItem);
                            player.currentlyHolding = false;
                            player.currentItem = new GameObject();
                            player.currentItem.name = "Empty";
                            Debug.Log("matt Cleaned");
                            farmer.check++;

                        }
                    }
                    else
                    {
                        timer = 0;
                    }
                }
            }
        }
/////////////////////////////////////////////////////////////////////////////
    }
}
