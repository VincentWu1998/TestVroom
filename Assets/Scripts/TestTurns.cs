using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurns : MonoBehaviour {
    public List<GameObject> arrows;
    public bool notSelected = true;

    // Use this for initialization
    void Start () {
        List<GameObject> arrows = new List<GameObject>();

    }

    void OnTriggerEnter(Collider other)
    {
        // If we are hitting an arrow
        if (other.gameObject.tag == "arrowCollidable")
        {
            Debug.Log("ARROW has collided, we have " + arrows.Count + " Arrows HIT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // Add collided arrows into an array
            arrows.Add(other.gameObject);
        }
    }

    // While we have collided and still colliding
    private void OnTriggerStay(Collider other)
    {
        // There should always be 3 collided arrows when we choose the arrow to randomly display
        if (other.gameObject.tag == "arrowCollidable" && arrows.Count == 3 && notSelected)
        {            
            // randomChoice selected
            int randomChoice = Random.Range(0, arrows.Count);
            // fetch randerer
            Renderer rend = arrows[randomChoice].GetComponent<Renderer>();
            //Set the main Color of the Material to green
            rend.material.color = Color.green;
            notSelected = false;
        }
    }

    // As we exit all the arrows, we will remove them from the list
    private void OnTriggerExit(Collider other) 
    {    
        if (other.gameObject.tag == "arrowCollidable" && arrows.Count > 0)
        {
            for (int i = 0; i < arrows.Count; i++)
            {
                if (other.gameObject.GetInstanceID() == arrows[i].GetInstanceID())
                {
                    Renderer rend = arrows[i].GetComponent<Renderer>();
                    rend.material.color = Color.white;
                    arrows.RemoveAt(i);
                    
                    if (arrows.Count == 0)
                    {
                        notSelected = true;
                    }
                    break;
                }
            }
        }        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
