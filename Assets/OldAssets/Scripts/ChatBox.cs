using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public static void Create(string text)
    {
        GameObject chatboxPrefab = Resources.Load<GameObject>("Prefabs/Chatbox");

        // Instantiate the chatbox prefab
        GameObject chatboxInstance = Instantiate(chatboxPrefab);

        TMP_Text textMesh = chatboxInstance.GetComponentInChildren<TMP_Text>();
        if (textMesh != null)
        {
            textMesh.text = text;
        }
        else
        {
            Debug.LogError("TMP_Text component not found in the Chatbox prefab!");
        }

    }
}
