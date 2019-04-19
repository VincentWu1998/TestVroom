using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crash : MonoBehaviour
{

    public Text crashText;

    // Use this for initialization
    void Start()
    {
        crashText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("crashed"))
        {
            CrashText();
        }
    }

    void CrashText()
    {
        crashText.text = "You Crashed! Game Over";
    }
}