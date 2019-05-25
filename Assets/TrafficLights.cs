using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TrafficLights : MonoBehaviour {
    private List<Light> reds = new List<Light>();
    private List<Light> yellows = new List<Light>();
    private List<Light> greens = new List<Light>();

    private bool ran = false;

    private int curr = 0;

    private int nextUpdate = Mathf.FloorToInt(Time.time);

    // Use this for initialization 
    void Start ()
    {
        if ( !ran )
        {
            ran = true;

            foreach ( Light light in this.GetComponentsInChildren<Light>())
            {
                light.enabled = false;
                if (light.name.IndexOf("rLight") != -1)
                {
                    reds.Add(light);
                } else if (light.name.IndexOf("yLight") != -1)
                {
                    yellows.Add(light);
                } else if (light.name.IndexOf("gLight") != -1)
                {
                    greens.Add(light);
                }
            } 
        }
    }

    // Update is called once per frame 
    void Update ()
    {
        if (Time.time >= nextUpdate)
        {
            if (Time.time >= nextUpdate)
            {
                curr = (curr +1) % 15;
                updateLights();

                nextUpdate++;
            }
        }

    }

    void updateLights()
    {
        /* 0-5 --> green 4 sec 
         * 5-8 --> yellow 3 sec 
         * 8-12 --> red 4 sec */
        
        // Reds
        if (curr - 11 >= 0)
        {
            foreach (Light light in reds)
            {
                light.color = Color.Lerp(Color.red, Color.red, 8);
                light.enabled = true;
            }
            foreach (Light light in yellows)
            {
                light.enabled = false;
            }
        }
        // Yellows
        else if (curr - 8 >= 0)
        {
            foreach(Light light in yellows)
            {
                light.color = Color.Lerp(Color.yellow, Color.yellow, 8);
                light.enabled = true;
            }
            foreach (Light light in greens)
            {
                light.enabled = false;
            }
        }
        // Greens
        else
        {
            foreach(Light light in greens)
            {
                light.color = Color.Lerp(Color.green, Color.green, 8);
                light.enabled = true;
            }
            foreach(Light light in reds)
            {
                light.enabled = false;
            }
        }
    }
}