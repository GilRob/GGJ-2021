using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text text;
    float timer = 0f;
    public Material cleanToilet;
    public Material cleanMatt;
    public GameObject gasCanister;
    private bool gasFull = false;
    public RawImage fade;
    private bool fadein = false;
    //public int[] dialogues;
    //public int element;
    ///////////////////////////////////

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        farmer = GameObject.FindObjectOfType<Farmer>();
        pMovement = GameObject.FindObjectOfType<CharacterController>();

        taskList = new bool[7];

        for (int i = 0; i < 7; i++)
        {
            taskList[i] = false;
        }

        fade.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(player.currentItem);
        ////////////////////////////////////////////////////////////////////////////////////
        //Bring Axe to Farmer Task
        //Debug.Log(dialogues.activeTask);
        if (player.hitInfo.collider != null)
        {
            if (player.hitInfo.collider.tag == "Farmer")
            {
                if (Input.GetButtonDown("Interact") && player.currentItem.name == "Axe" && farmer.inside == true)
                {
                    Debug.Log("Respect Fam");
                    taskList[0] = true;
                    Destroy(player.currentItem);
                    player.currentlyHolding = false;
                    player.currentItem = new GameObject();
                    player.currentItem.name = "Empty";
                    farmer.check++;
                    //element = Random.Range(0, dialogues.Count);
                    farmer.Dialogue.Show("My axe...Need you to do a few more things", "Farmer", Random.Range(5, 7), false);
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
        //////
        if (dialogues.activeTask == 7)
        {
            if (player.hitInfo.collider != null)
            {
                if (player.hitInfo.collider.name == "Garb")
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        Destroy(player.hitInfo.collider.gameObject);
                        player.currentlyHolding = false;
                        player.currentItem = new GameObject();
                        player.currentItem.name = "Empty";
                        gasCanister.tag = "Interactable";
                        dialogues.Show("Yo that's crazy still", "Player", 4, true);
                    }
                }
            }
            //if(player.currentItem.name == "Empty" && player.hitInfo.collider.name == "GasCan")
            //{
            //    player.hitInfo.collider.tag = "Interactable";
            //    Debug.Log("Siphon");
            //}
            if (player.currentItem.name == "GasCan" && player.hitInfo.collider.name == "Car")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        gasFull = true;
                        Debug.Log("Siphon");

                    }
                }
                if (player.currentItem.name == "GasCan" && player.hitInfo.collider.name == "Boat" && gasFull == true)
                {
                    Debug.Log("End");
                }
            }
            if (player.currentItem.name == "GasCan" && player.hitInfo.collider.name == "Boat" && gasFull == true)
            {
                Debug.Log("END");
                SceneManager.LoadScene("WinScene");
            }

        }
        ////
        if (player.hitInfo.collider != null)
        {
            if (player.hitInfo.collider.name == "FarmSign")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("'Hatbek Dream Farms, Fresh Local &  \u0022Fun\u0022!' A little ominous if anything", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "poop")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Issa poop", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "Cow")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Sup cow", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "rickyPainting")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Hmph", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "ChickenWithAnim (4)")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Sup chick", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "Boat")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Wonder if this is working...", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "eashPainting")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Getting a lot of negative vibes from this", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "fossilPainting")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Kinda cute not gonna lie", "Player", 4, true);
                }
            }
            if (player.hitInfo.collider.name == "RockingChair")
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogues.Show("Of course this man is gonna have a rocking chair", "Player", 4, true);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //Text Elements

        if (player.hitInfo.collider != null)
        {
            //Pickup Items////////////////////////////////////////////////////////////////////////////////////
            if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "Axe")
            {
                text.text = "Press E to Pickup Axe";
            }
            else if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "broom" && dialogues.activeTask == 2)
            {
                text.text = "Press E to Pickup Broom";
            }
            else if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "toiletBrush" && dialogues.activeTask == 1)
            {
                text.text = "Press E to Pickup Toilet Brush";
            }
            else if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "bucket" && dialogues.activeTask == 3)
            {
                text.text = "Press E to Pickup Bucket";
            }
            else if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "ChickenCoop" && taskList[3] == false && dialogues.activeTask == 4)
            {
                text.text = "Press E to Pickup Egg";
            }
            else if (player.currentItem.name == "Empty" && player.hitInfo.collider.name == "pitchFork" && dialogues.activeTask == 6)
            {
                text.text = "Press E to Pickup Pitchfork";
            }

            ///////////////////////////////////////////////////////////////////////////////////////////
            else if (player.currentItem.name == "Axe" && player.hitInfo.collider.tag == "Farmer")
            {
                text.text = "Press E to Give Axe";
            }
            else if (player.currentItem.name == "broom" && player.hitInfo.collider.name == "matt" && dialogues.activeTask == 2)
            {
                text.text = "Hold E to Sweep Mat";
            }
            else if (player.currentItem.name == "toiletBrush" && player.hitInfo.collider.name == "toilet" && dialogues.activeTask == 1)
            {
                text.text = "Hold E to Clean Toilet";
            }
            else if (player.currentItem.name == "bucket" && player.hitInfo.collider.name == "ChickenCoop" && dialogues.activeTask == 3)
            {
                text.text = "Press E to Feed Chickens";
            }
            else if (player.currentItem.name == "Egg(Clone)" && player.hitInfo.collider.name == "EggBasket" && dialogues.activeTask == 4)
            {
                text.text = "Press E to Place Eggs In Basket";
            }
            else if (player.currentItem.name == "pitchFork" && player.hitInfo.collider.name == "poop" && dialogues.activeTask == 6)
            {
                text.text = "Hold E to Clean Poop";
            }
            else if (Vector3.Distance(this.transform.position, Hayroll.transform.position) <= 3f && taskList[1] == false && dialogues.activeTask == 5)
            {
                text.text = "Move Forward to Roll Hay Towards the Barn";
            }
            else if(dialogues.activeTask == 7)
            {
                if (player.hitInfo.collider.name == "Garb")
                {
                    text.text = "Press E to Pickup Trash";
                }
                else if(player.hitInfo.collider.name == "GasCan" && gasCanister.tag == "Interactable")
                {
                    text.text = "Press E to Pickup Gas Can";
                }
                else if(player.currentItem.name == "GasCan" && player.hitInfo.collider.name == "Car" && gasFull == false)
                {
                    text.text = "Press E to Siphon";
                }
                else if(player.currentItem.name == "GasCan" && player.hitInfo.collider.name == "Boat" && gasFull == true)
                {
                    text.text = "Press E to Escape";
                }
                else
                {
                    text.text = "";
                }
            }
            else
            {
                text.text = "";
            }
        }
    }

    void fadeIn()
    {
        fade.CrossFadeAlpha(1, 2, false);
    }
    //////////////////////////////////////////////////////////////////////////////
}
