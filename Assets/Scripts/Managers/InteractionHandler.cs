using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionHandler : MonoBehaviour
{
    // Enum of three states for this object, it'll either be a NPC, Object, or an Objective.
    public enum SearchType { NPC, Object, Objectives }
    // Eventually, we'll use searchType for unique setups for dialogue or dialogue boxes/choices.
    [SerializeField] private SearchType searchType;
    // Decide whether the interaction will be dialogue or not
    [SerializeField] private bool hasDialogue = false;
    // List of string lines shown in the inspector
    [SerializeField] private List<string> dialogueLines = new List<string>();
    // Prefab of chatbox that'll appear when dialogue is ready.
    [SerializeField] private GameObject chatBoxPrefab;

    // Reference that tracks the current line of dialogue
    private int currentLineIndex = 0;

    // Called by the detection manager to interact with the object on E press.
    public void Interact()
    {
        // If dialogue is allowed, the chatbox exists, and the total dialogue lines are more than zero, do a dialogue interaction
        if (hasDialogue && chatBoxPrefab != null && dialogueLines.Count > 0)
        {
            // Deleting any previous chatbox instances
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("Chatbox");

            // For each box currently found, delete them all
            foreach (GameObject box in boxes)
            {
                Destroy(box);
            }

            // Create a new ChatBox instance
            GameObject newChatBox = Instantiate(chatBoxPrefab);

            // Find the TMP_Text component as a child of the ChatBox
            TMP_Text chatText = newChatBox.GetComponentInChildren<TMP_Text>();
            if (chatText != null)
            {
                Debug.Log($"Displayed Dialogue: {dialogueLines[currentLineIndex]}");
                chatText.text = dialogueLines[currentLineIndex];
                currentLineIndex = (currentLineIndex + 1) % dialogueLines.Count;
                //DisplayDialogue(dialogueLines[currentLineIndex]);
            }
            else
            {
                Debug.LogError("No TMP_Text found as a child of the ChatBox!");
            }
        }
        else
        {
            Debug.LogWarning("Interaction available, but no dialogue is set.");
        }
    }

    public void NextLine() {
        // If the total dialogue lines are more than zero
        if (dialogueLines.Count > 0) {
            currentLineIndex = (currentLineIndex + 1) % dialogueLines.Count;
            Debug.Log($"Moving to line {currentLineIndex}: {dialogueLines[currentLineIndex]}");
        }
    }

    public void SetLine(int index) {
        // If index is more than or equal to 0 and isn't more than the total lines.
        if (index >=0 && index < dialogueLines.Count) {
            currentLineIndex = index;
            Debug.Log($"Setting dialogue to the line {currentLineIndex}: {dialogueLines[currentLineIndex]}");
        }
    }
}
