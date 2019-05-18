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

    // Use this for initialization
    void Start()
    {

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

	public IEnumerator addScore(){
		collisionCounter++;
		nextFire = Time.time + waitTime;
		Debug.Log ("score is now " + collisionCounter);
		crashed = false;
		yield return new WaitForSeconds (waitTime);
	}
}