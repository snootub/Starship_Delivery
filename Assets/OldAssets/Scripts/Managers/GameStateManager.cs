
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [SerializeField]
    public string currentGameState = "default";

    public GameObject PlatformParent;
    public GameObject Player;

    private PlayerMovement pMove; 
    private ConstantForce pGrav;

    public Camera camera2D;
    public Camera camera3D;

    public GameObject playerCharacter;

    private static List<string> gameStates = new List<string> {"Moving Platforms", "Bouncy Platforms", "Trampolines", "2D Platformer", "Bigger Platforms", "Smaller Platforms", "High Gravity", "Low Gravity", "BirdMode", "Ice"};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Debug.Log(gameStates);

        pMove = Player.GetComponent<PlayerMovement>();
        pGrav = Player.GetComponent<ConstantForce>();
    }


    public void ChangeGameState()
    {
        resetGame();

        int index = UnityEngine.Random.Range(0, gameStates.Count);

        Debug.Log(string.Format("Changed Game state to: {0}", gameStates[index]));

        currentGameState = gameStates[index];

        switch (currentGameState)
        {
            case "Moving Platforms":
                makeMoving();
                break;
            case "Bouncy Platforms":
                makeBouncy();
                break;
            case "Trampolines":
                makeTrampolines();
                break;
            case "Bigger Platforms":
                changePlatformSize(10);
                break;
            case "Smaller Platforms":
                changePlatformSize(-5);
                break;
            case "2D Platformer":
                changeTo2DPlatformer();
                break;
            case "High Gravity":
                pGrav.force = new Vector3(0, -2, 0);
                break;
            case "Low Gravity":
                pGrav.force.Set(0, 4, 0);
                break;
            case "Birdmode":
                pMove.moveSpeed = 5;
                pMove.airMultiplier = 2.4f;
                break;
            case "Ice":
                pMove.groundDrag = .01f;
                break;

            default:
                break;
        }


    }

    private void resetGame()
    {
        switch (currentGameState)
        {
            case "Bouncy Platforms":
                break;

            case "2D Platformer":
                Debug.Log("resetting game");
                camera2D.GetComponent<Camera>().enabled = false;
                camera3D.GetComponent<Camera>().enabled = true;
                camera3D.GetComponent<PlayerCam>().enabled = true;

                pMove.changeControls(false);
                pMove.resetOrientation();
                currentGameState = "default";

                foreach(Transform platform in PlatformParent.transform)
                {
                    
                    foreach (Transform cube in platform.transform)
                    {
                        if (cube.tag == "Platform")
                        {
                            cube.GetComponent<Transform>().localScale -= new Vector3(0, 0, 50);
                        }
                    }
                }
                break;

            case "Bigger Platforms":
                changePlatformSize(-10);
                break;
            case "Smaller Platforms":
                changePlatformSize(5);
                break;
            case "High Gravity":
                pGrav.force.Set(0, 0, 0);
                break;
            case "Low Gravity":
                pGrav.force.Set(0, 0, 0);
                break;
            case "Birdmode":
                pMove.moveSpeed = 9;
                pMove.airMultiplier = .4f;
                break;
            case "Ice":
                pMove.groundDrag = 6;
                break;
            default:
                break;

        }
    }

    private void changePlatformSize(float multiplier)
    {
        foreach(Transform platform in PlatformParent.transform)
        {

            foreach (Transform cube in platform.transform)
            {
                if (cube.tag == "Platform")
                {
                    cube.GetComponent<Transform>().localScale += new Vector3(multiplier, 0, multiplier);
                }
            }

        }
    }
    private void changeTo2DPlatformer()
    {
        camera2D.GetComponent<Camera>().enabled = true;
        camera3D.GetComponent<Camera>().enabled = false;
        camera3D.GetComponent<PlayerCam>().enabled = false;

        pMove.changeControls(true);
        pMove.resetOrientation();


        foreach (Transform platform in PlatformParent.transform)
        {
            foreach (Transform cube in platform.transform)
            {
                if (cube.tag == "Platform")
                {
                    cube.GetComponent<Transform>().localScale += new Vector3(0, 0, 50);
                }
            }
            
        }


    }

    private void makeMoving()
    {

        float chance = 0.3f;

        foreach (Transform platform in PlatformParent.transform)
        {
            //Reset bouncy platforms
            platform.GetComponent<PlatformManager>().moveRangeX = 0.0f;
            platform.GetComponent<PlatformManager>().moveSpeed = 0.0f;


            float roll = UnityEngine.Random.Range(0.0f, 1.0f);
            if (roll < chance)
            {
                platform.GetComponent<PlatformManager>().moveRangeX = UnityEngine.Random.Range(6.0f, 24.0f);
                platform.GetComponent<PlatformManager>().moveSpeed = UnityEngine.Random.Range(1.0f, 3.0f);

            }
        }
    }
    private void makeBouncy()
    {
        
        float chance = 0.6f;

        foreach (Transform platform in PlatformParent.transform)
        {
            //Reset bouncy platforms
            platform.GetComponent<PlatformManager>().bounciness = 0.0f;

            foreach (Transform cube in platform.transform)
            {
                if (cube.tag == "Platform")
                {
                    cube.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                }
            }

            float roll = UnityEngine.Random.Range(0.0f, 1.0f);
            if (roll < chance)
            {
                platform.GetComponent<PlatformManager>().bounciness = 0.6f;

                foreach (Transform cube in platform.transform)
                {
                    if (cube.tag == "Platform")
                    {
                        cube.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
                    }
                }
            }
        }
    }
    private void makeTrampolines()
    {

        float chance = 0.1f;

        foreach (Transform platform in PlatformParent.transform)
        {
            

            float roll = UnityEngine.Random.Range(0.0f, 1.0f);
            if (roll < chance)
            {
                platform.GetComponent<PlatformManager>().bounciness = 1.1f;

                foreach (Transform cube in platform.transform)
                {
                    if (cube.tag == "Platform")
                    {
                        cube.GetComponent<Renderer>().material.color = new Color(0, 1.6f, 0);
                    }
                }
            }
        }
    }

}
