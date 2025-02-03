using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pause;
    public GameObject options;
    public PlayerMovement pm; 
    public GameObject player; 
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            options.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pm.enabled = false;
            Time.timeScale = 0f;
        }    
    }

    public void BackButtonPause(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause.SetActive(false);
        Time.timeScale = 1;
        pm.enabled =true;
        options.SetActive(false);
    }
    public void BackButtonOptions(){
        options.SetActive(false);
        pause.SetActive(true);
    }

    public void ExitButtonPause(){
        Application.Quit();
    }

    public void OptionsButtonPause(){
        pause.SetActive(false);
        options.SetActive(true);
    }
    public void SaveGame(){
        Debug.Log(pos);
        SaveSystem.SavePlayer(pos);
    }
    public void LoadGame(){
        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.position[0]);
        Debug.Log(data.position[1]);
        Debug.Log(data.position[2]);
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
