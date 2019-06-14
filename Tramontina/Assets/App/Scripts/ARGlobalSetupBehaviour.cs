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
        public Sprite[] m_premios;
        public Sprite[] m_Sprite;
        Sprite image, premio;
        public GameObject[] centros;
        public GameObject roulette;
        public GameObject Target;
        public GameObject ContainerTarget;
        public GameObject activityImage;
        public GameObject messageImage;
        public AudioSource rouletteSound;
        public AudioSource winSound;
        public Button playButton;
        public Button clearButton;
        public GameObject clearObject;
        public float timeActiveOnTargetLose = .1f;
        float timeRotation = 5f;
        float timeRotationmax = 10.1f;
        float timeRotationmin = 5.5f;

        bool detected = false;
        float timer = 0;
        float timer2 = 0;
        float turnSpeedRef = 1000;
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
                //Debug.LogError(title + " " + keyMessage);
            }
        }

        void Start()
        {
            centros[0].SetActive(true);
            messageImage.SetActive(true);
            activityImage.SetActive(false);
            playButton.onClick.AddListener(Rot);
            clearButton.onClick.AddListener(Rot);
            clearObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Target.activeSelf == true)
            {
                timer = 0f;
                activityImage.SetActive(false);
                clearObject.SetActive(false);
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
                        clearObject.SetActive(true);
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
                    //turnSpeed = turnSpeedRef * Mathf.Exp(-(timer2 / (timeRotation / 5)));
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
                    clearObject.SetActive(true);
                    rouletteSound.Stop();
                    winSound.Play();

                    foreach (GameObject centro in centros)
                    {
                        centro.SetActive(false);
                    }
                    float[] ang = { 0, 20, 65, 109, 140, 186, 226, 257.6f, 303.48f, 346.5f, 360 };
                    centros[0].SetActive(true);
                    //print(roulette.transform.localEulerAngles.y);

                    if ((roulette.transform.localEulerAngles.y >= ang[0]) && (roulette.transform.localEulerAngles.y < ang[1]))
                    {
                        centros[1].SetActive(true);
                        image = m_Sprite[1];
                        premio = m_premios[1];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[1]) && (roulette.transform.localEulerAngles.y < ang[2]))
                    {
                        centros[2].SetActive(true);
                        image = m_Sprite[2];
                        premio = m_premios[2];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[2]) && (roulette.transform.localEulerAngles.y < ang[3]))
                    {
                        centros[3].SetActive(true);
                        image = m_Sprite[3];
                        premio = m_premios[3];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[3]) && (roulette.transform.localEulerAngles.y < ang[4]))
                    {

                        centros[4].SetActive(true);
                        image = m_Sprite[4];
                        premio = m_premios[4];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[4]) && (roulette.transform.localEulerAngles.y < ang[5]))
                    {
                        centros[5].SetActive(true);
                        image = m_Sprite[5];
                        premio = m_premios[5];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[5]) && (roulette.transform.localEulerAngles.y < ang[6]))
                    {
                        centros[6].SetActive(true);
                        image = m_Sprite[6];
                        premio = m_premios[6];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[6]) && (roulette.transform.localEulerAngles.y < ang[7]))
                    {
                        centros[7].SetActive(true);
                        image = m_Sprite[7];
                        premio = m_premios[7];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[7]) && (roulette.transform.localEulerAngles.y < ang[8]))
                    {
                        centros[8].SetActive(true);
                        image = m_Sprite[8];
                        premio = m_premios[8];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[8]) && (roulette.transform.localEulerAngles.y < ang[9]))
                    {
                        centros[9].SetActive(true);
                        image = m_Sprite[9];
                        premio = m_premios[9];
                    }
                    if ((roulette.transform.localEulerAngles.y >= ang[9]) && (roulette.transform.localEulerAngles.y <= ang[10]))
                    {
                        centros[1].SetActive(true);
                        image = m_Sprite[1];
                        premio = m_premios[1];
                    }
                    messageImage.GetComponent<Image>().overrideSprite = image;
                    activityImage.GetComponent<Image>().overrideSprite = premio;
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
                messageImage.GetComponent<Image>().overrideSprite = m_Sprite[0];
                activityImage.GetComponent<Image>().overrideSprite = m_premios[0];

                foreach (GameObject centro in centros)
                {
                    centro.SetActive(false);
                }
                centros[0].SetActive(true);
                rouletteSound.volume = 1f;
                rouletteSound.Stop();
                rouletteSound.Play();
                timer2 = 0;
                rotate = true;
                turnSpeed = turnSpeedRef;
                timeRotation = UnityEngine.Random.Range(timeRotationmin, timeRotationmax);
                //Debug.Log("Last Angle: " + roulette.transform.localEulerAngles.y);
                clearObject.SetActive(false);
            }
            else if(Target.activeSelf == false && rotate == false)
            {
                
                messageImage.GetComponent<Image>().overrideSprite = m_Sprite[0];
                activityImage.GetComponent<Image>().overrideSprite = m_premios[0];
                foreach (GameObject centro in centros)
                {
                    centro.SetActive(false);
                }
                centros[0].SetActive(true);
                clearObject.SetActive(false);
            }

            seeActivityImage = false;
        }

        void Rotate(GameObject component, float angle)
        {
            component.transform.Rotate(0, angle, 0, Space.Self);
        }


        //=============================================================================================================================


    }

}
