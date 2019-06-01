using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigureMenuStuff : MonoBehaviour {

    // GameObject gameObjectYo;
    public GameObject script1;
    public GameObject script2;
    public GameObject script3;
    public GameObject script4;

	// Use this for initialization
	void Start () {
        // gameObjectYo = new GameObject();
        //GameObject.Find("Script1");
        script1.SetActive(true);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void PlayDemo()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        script1.SetActive(true);
        script2.SetActive(false);
        script3.SetActive(false);
        script4.SetActive(false);
        script4.SetActive(false);
        SceneManager.LoadScene(0);
        
    }

    public void NextBtn()
    {
        //GameObject game = new GameObject();
        //script2 = new GameObject();

        if (script1.activeInHierarchy == true)
        {
            script1.SetActive(false);
            script2.SetActive(true);
        }
        
        else if(script2.activeInHierarchy == true)
        {
            script2.SetActive(false);
            script3.SetActive(true);
        }

        else if (script3.activeInHierarchy == true)
        {
            script3.SetActive(false);
            script4.SetActive(true);
        }
    }

    public void PrevBtn()
    {
        //GameObject game = new GameObject();
        //script2 = new GameObject();

        if (script2.activeInHierarchy == true)
        {
            script2.SetActive(false);
            script1.SetActive(true);
        }

        else if (script3.activeInHierarchy == true)
        {
            script3.SetActive(false);
            script2.SetActive(true);
        }

        else if (script4.activeInHierarchy == true)
        {
            script4.SetActive(false);
            script3.SetActive(true);
        }
    }
}
