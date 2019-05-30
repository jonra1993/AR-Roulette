using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=iCEQdP1TdwM  unity analitics

public class GameManager : MonoBehaviour {
    private string id;
    private bool mem = false;
    private bool show = false;
    private bool clickedBefore = false;
    private string textMessage = "";
    public static int orientation = 1;

    public GameObject switchCamera, settings;

    // Use this for initialization
    void Start() {
        id = PlayerPrefs.GetString("ID", "");
        Debug.Log(id);
    }

    private void Update()
    {
        //SceneManager.GetActiveScene().name == "argumented"
        if (SceneManager.GetActiveScene().buildIndex==2 && mem==false)
        {
            Debug.Log("nuevo Usuario?");
            if (id == "")
            {
                string guid = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString("ID", guid);
                id = guid;
                Debug.Log("si es nuevo");
                Invoke("Function", 1.0f); //invoca el evento luego de 1 segundos
            }
            mem = true;
        }

        //Check input for the first time
        if (Input.GetKeyDown(KeyCode.Escape) && !clickedBefore)
        {
            textMessage = "Presione nuevamente para salir..";
            //Debug.Log("Back Button pressed for the first time");
            //Set to false so that this input is not checked again. It will be checked in the coroutine function instead
            clickedBefore = true;

            //Activate Quit Object
            show = true;

            //Start quit timer
            StartCoroutine(quitingTimer());
        }

        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            orientation = 1;
            //blink.SetActive(false);
            //show = false;
        }
        else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            orientation = 2;
            Screen.orientation = ScreenOrientation.Portrait;
            //blink.SetActive(true);
            //show = true;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            orientation = 3;
            Screen.orientation = ScreenOrientation.Portrait;
            //blink.SetActive(true);
            //show = true;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            orientation = 4;
            Screen.orientation = ScreenOrientation.Portrait;
            //blink.SetActive(true);
            //show = true;
        }
    }

    IEnumerator quitingTimer()
    {
        //Wait for a frame so that Input.GetKeyDown is no longer true
        yield return null;

        //3 seconds timer
        const float timerTime = 3f;
        float counter = 0;

        while (counter < timerTime)
        {
            //Increment counter while it is < timer time(3)
            counter += Time.deltaTime;

            //Check if Input is pressed again while timer is running then quit/exit if is
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //textMessage = "Back Button pressed for the second time. EXITING.....";
                //Debug.Log("Back Button pressed for the second time. EXITING.....");
                Quit();
            }

            //Wait for a frame so that Unity does not freeze
            yield return null;
        }

        //Debug.Log("Timer finished...Back Button was NOT pressed for the second time within: '" + timerTime + "' seconds");

        //Timer has finished and NO QUIT(NO second press) so deactivate
        show = false;
        //Reset clickedBefore so that Input can be checked again in the Update function
        clickedBefore = false;
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    //Application.Quit();
    System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }


    void Function() {
        print("GameManager iniciado");
    
    }

    private void OnGUI()
    {
        if (show)
        {
            GUIStyle scoreStyle = new GUIStyle("box");
            scoreStyle.fontSize = 30;
            scoreStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(10, Screen.height / 2, Screen.width - 20, 50), textMessage,scoreStyle);
        }
            
    }


}
