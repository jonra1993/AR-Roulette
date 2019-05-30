//=============================================================================================================================
//
// Copyright (c) 2015-2018 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//

//https://www.zapsplat.com/?s=wheel+of+fortune&post_type=music&sound-effect-category-id=
//=============================================================================================================================

//https://www.youtube.com/watch?v=cW8JxgtjFFU
//https://stackoverflow.com/questions/45226800/download-assetbundle-in-hard-disk
//https://insights.nimblechapps.com/unity/implement-assetbundles-in-unity
//

using System.Collections;
using UnityEngine;
using System.Linq;
using EasyAR;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Sample
{
    public class ARGlobalSetupBehaviour : MonoBehaviour
    {
        public GameObject roulette;
        public GameObject Target;
        public GameObject ContainerTarget;
        public GameObject activityImage;
        public GameObject messageImage;
        public AudioSource rouletteSound;
        public Button playButton;
        public float timeActiveOnTargetLose = .1f;
        float timeRotation = 5f;
        float timeRotationmax = 10f;
        float timeRotationmin = 7f;


        bool detected = false;
        float timer = 0;
        float timer2 = 0;
        float turnSpeedRef = 500;
        float turnSpeed = 0;
        bool rotate=false;
        bool seeActivityImage = false;


        private const string title = "Please enter KEY first!";
        private const string boxtitle = "===PLEASE ENTER YOUR KEY HERE===";
        private const string keyMessage = ""
            + "Steps to create the key for this sample:\n"
            + "  1. login www.easyar.com\n"
            + "  2. create app with\n"
            + "      Name: HelloARTarget (Unity)\n"
            + "      Bundle ID: cn.easyar.samples.unity.helloartarget\n"
            + "  3. find the created item in the list and show key\n"
            + "  4. replace all text in TextArea with your key";

        private void Awake()
        {
            if (FindObjectOfType<EasyARBehaviour>().Key.Contains(boxtitle))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
                Debug.LogError(title + " " + keyMessage);
            }
        }

        void Start()
        {
            messageImage.GetComponent<Image>().color = Color.black;
            messageImage.SetActive(true);
            activityImage.SetActive(false);
            playButton.onClick.AddListener(Rot);
        }

        // Update is called once per frame
        void Update()
        {
            if (Target.activeSelf == true)
            {
                timer = 0f;
                activityImage.SetActive(false);
            }
            else
            {
                if (timer < timeActiveOnTargetLose)
                {
                    ContainerTarget.transform.SetParent(this.transform);
                    timer += Time.deltaTime;

                }
                else
                {
                    ContainerTarget.transform.SetParent(Target.transform);
                    if (seeActivityImage == true)
                    {
                        activityImage.SetActive(true);
                    }
                }

            }


            if (rotate == true)
            {
                if (timer2 < timeRotation)
                {
                    float angl = Time.deltaTime * turnSpeed;
                    Rotate(roulette, angl);   //delta time  aproximadamente 0,016
                    timer2 += Time.deltaTime;
                    //Debug.Log("Delta Time: " + Time.deltaTime + "Angle: "+ roulette.transform.localEulerAngles.y);
                    //turnSpeed = turnSpeed - 1;
                    turnSpeed = turnSpeedRef*(1 - (1 / timeRotation) * timer2);
                    float atenuation= Mathf.Exp(-(timer2/(timeRotation/5)));
                    if (atenuation < 0) atenuation = 0;
                    rouletteSound.volume = atenuation;
                    if (turnSpeed < 0)
                    {
                        rouletteSound.Stop();
                        turnSpeed = 0;
                    }
                    
                }
                else
                {
                    rotate = false;
                    rouletteSound.Stop();
                    Color col=Color.blue;
                    if (((roulette.transform.localEulerAngles.y>=0)&& (roulette.transform.localEulerAngles.y < 40))||(roulette.transform.localEulerAngles.y == 40)) col= Color.magenta;
                    if ((roulette.transform.localEulerAngles.y>=40)&& (roulette.transform.localEulerAngles.y < 80)) col = Color.blue;
                    if ((roulette.transform.localEulerAngles.y >= 80) && (roulette.transform.localEulerAngles.y < 120)) col = Color.black;
                    if ((roulette.transform.localEulerAngles.y >= 120) && (roulette.transform.localEulerAngles.y < 160)) col = Color.cyan; 
                    if ((roulette.transform.localEulerAngles.y >= 160) && (roulette.transform.localEulerAngles.y < 200)) col = Color.gray;
                    if ((roulette.transform.localEulerAngles.y >= 200) && (roulette.transform.localEulerAngles.y < 240)) col = Color.green;
                    if ((roulette.transform.localEulerAngles.y >= 240) && (roulette.transform.localEulerAngles.y < 280)) col = Color.red;
                    if ((roulette.transform.localEulerAngles.y >= 280) && (roulette.transform.localEulerAngles.y < 320)) col = Color.yellow;
                    if ((roulette.transform.localEulerAngles.y >= 320) && (roulette.transform.localEulerAngles.y < 360)) col = Color.white;
                    messageImage.GetComponent<Image>().color = col;
                    activityImage.GetComponent<Image>().color = col;
                    seeActivityImage = true;
                }
            }


        }
        //=============================================================================================================================
        // This functions makes to rotate the AR-Roulette

        public void Rot()
        {
            if (Target.activeSelf == true)
            {
                rouletteSound.volume = 1f;
                rouletteSound.Stop();
                rouletteSound.Play();
                timer2 = 0;
                rotate = true;
                turnSpeed = turnSpeedRef;
                timeRotation = UnityEngine.Random.Range(timeRotationmin, timeRotationmax);

                //Debug.Log("Last Angle: " + roulette.transform.localEulerAngles.y);
            }
        }

        void Rotate(GameObject component, float angle)
        {
            component.transform.Rotate(0, angle, 0, Space.Self);
        }


        //=============================================================================================================================


    }

}
