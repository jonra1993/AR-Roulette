using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public string url="";
    private string objectName;
    // Use this for initialization
    void Start()
    {
        objectName = this.gameObject.name;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Code for OnMouseDown in the iPhone. Unquote to test.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == objectName)
                {
                    print(hit.transform.name);
                    try
                    {
                        if(url!=""&& Application.internetReachability != NetworkReachability.NotReachable) Application.OpenURL(url);
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }
    }
}
