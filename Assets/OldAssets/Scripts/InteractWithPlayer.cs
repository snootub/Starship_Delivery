using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractWithPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Plays tutorial speech
    private void Awake()
    {
        ChatBox.Create("Hello there! Welcome to your new job where you deliver ingredients to this truck. Please step on this pressure plate for more info.");
    }
    
    // When on the pressure plate, play current dialogue message.
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player"))
        {
            // Deleting any previous chatboxes
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("Chatbox");

            foreach (GameObject box in boxes)
            {
                Destroy(box);
            }

            // Get the current order's name using GetOrder
            string currentOrderName = player.GetComponent<OrderHandler>().GetOrder();

            // Log the order to the console
            Debug.Log("Current Order: " + currentOrderName);
            if (currentOrderName == "IngredientDropOff")
            {
                ChatBox.Create("Thank you for delivering the ingredient! Press E for your next mission.");
            }
            else
            {
                ChatBox.Create("Please deliver me a " + currentOrderName + ". Please return on this pressure plate for the next ingredient.");
            }
        }
    }

    // When off the pressure plate, delete chatbox
    void OnTriggerExit(Collider other)
    {
        // Deleting any previous chatboxes
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Chatbox");

        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }
    }

    private void Update()
    { // For players needing next mission on pressure plate + debugging with interacting with objects/npcs from a range
        if(Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 1f;
            // Creates a sphere around the pressure plate and checks for colliders nearby
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                Debug.Log(collider); // Debug information of objects within range

                // Check if the collider has the Player tag or component
                if (collider.CompareTag("Player"))
                {
                    // Delete any previous chatboxes
                    GameObject[] boxes = GameObject.FindGameObjectsWithTag("Chatbox");
                    foreach (GameObject box in boxes)
                    {
                        Destroy(box);
                    }

                    // Get the current order's name using GetOrder
                    string currentOrderName = player.GetComponent<OrderHandler>().GetOrder();

                    // Create the chatbox with the current order message
                    ChatBox.Create("Please deliver me a " + currentOrderName + ". Please return on this pressure plate for the next ingredient.");
                }
            }
        }
    }
}
