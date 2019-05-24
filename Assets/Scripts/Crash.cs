using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crash : MonoBehaviour
{
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
	public int collisionCounter = 0;
	private float waitTime = 5.0F;
	private float nextFire = 0.0F;
	private bool crashed = false;
	public List<GameObject> arrows;
    public bool notSelected = true;

    // Use this for initialization
    void Start()
    {
		List<GameObject> arrows = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
		if (crashed == true && Time.time > nextFire) {
			StartCoroutine(addScore ());
			Debug.Log ("got here");
			Debug.Log (crashed);
			Debug.Log (collisionCounter);
		}
    }

    void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.tag == "collidable")
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * 250000);			
			crashed = true;	
			RealisticCarController.m_verticalInput = 0;
            frontPassengerW.motorTorque = 0;
            frontDriverW.motorTorque = 0;
            rearDriverW.motorTorque = 0;
            rearPassengerW.motorTorque = 0;			

        }
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
        if (other.gameObject.tag == "arrowCollidable" && arrows.Count >= 2 && notSelected)
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
                    rend.material = Resources.Load("Materials/Glass", typeof(Material)) as Material;
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

    public IEnumerator addScore(){
		collisionCounter++;
		nextFire = Time.time + waitTime;
		Debug.Log ("score is now " + collisionCounter);
		crashed = false;
		yield return new WaitForSeconds (waitTime);
	}
}