using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;

public class Crash : MonoBehaviour
{
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public Sprite LTurnSprite;
    public Sprite NTurnSprite;
    public Sprite RTurnSprite;
    public Sprite CTurnSprite;

    public static int collisionCounter = 0;
	private float waitTime = 5.0F;
	private float nextFire = 0.0F;
	public static bool crashed = false;
	public List<GameObject> arrows;
    public bool notSelected = true;
    public int endGameCounter = 0;
    private static Timer endGameTimer;
    public static bool endGameBool = false;
    public static int wentOfftrackCounter = 0;
    public static bool enterTrigger = false;
    public static bool stayTrigger = false;



    // Use this for initialization
    void Start()
    {
		List<GameObject> arrows = new List<GameObject>();
        endGameCounter = 0;
        endGameTimer = new System.Timers.Timer();
        endGameTimer.Interval = 10000;

        endGameTimer.Elapsed += OnTimedEvent;
        endGameTimer.AutoReset = true;
        

    }

    // Update is called once per frame
    void Update()
    {
		if (crashed == true && Time.time > nextFire) {
			StartCoroutine(addScore ());
			Debug.Log ("got here");
			Debug.Log (crashed);
			Debug.Log (collisionCounter);
            endGameTimer.Enabled = true;
        }

        if(endGameBool)
        {
            EndGame();
            endGameBool = false;
        }

        /*if(enterTrigger && (stayTrigger == false))
        {
            wentOfftrackCounter = wentOfftrackCounter + 1;
        }*/
        

    }

    private static void OnTimedEvent(object sender, ElapsedEventArgs e)
    { 
       if(RealisticCarController.carSpeed <= 0.01)
        {
            Debug.Log("ENDING GAME DUE TO INACTIVITY OR CRASH..................");
            endGameTimer.Enabled = false;
            Crash.endGameBool = true;
           
            
        }
        endGameTimer.Enabled = false;
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
           
           // addScore();

        }
	}

    void OnTriggerEnter(Collider other)
    {
        // If we are hitting an arrow
        if (other.gameObject.tag == "arrowCollidable" && arrows.Count < 3)
        {
            Debug.Log("ARROW has collided, we have " + arrows.Count + " Arrows HIT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // Add collided arrows into an array
            arrows.Add(other.gameObject);
            enterTrigger = true;
        }
    }

    // While we have collided and still colliding
    private void OnTriggerStay(Collider other)
    {
        // There should always be 3 collided arrows when we choose the arrow to randomly display
        if (other.gameObject.tag == "arrowCollidable" && arrows.Count >= 2 && notSelected)
        {
            stayTrigger = true;

            // randomChoice selected
            int randomChoice = Random.Range(0, arrows.Count);
            // fetch randerer
            Renderer rend = arrows[randomChoice].GetComponent<Renderer>();
            //Set the main Color of the Material to green
            rend.material = Resources.Load("Materials/GreenGlass", typeof(Material)) as Material;

            //arrows[randomChoice].GetComponent<MeshRenderer>().enabled = true;

            if (rend.name == "LTurn")
            {
                GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite = LTurnSprite;
                Debug.LogError("SET TO LEFT: " + GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite + " --- " + NTurnSprite + " " + LTurnSprite + " " + RTurnSprite);
            }
            else if (rend.name == "NoTurn")
            {
                GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite = NTurnSprite;
                Debug.LogError("SET TO NONE: " + GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite + " --- " + NTurnSprite + " " + LTurnSprite + " " + RTurnSprite);
            }
            else if (rend.name == "RTurn")
            {
                GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite = RTurnSprite;
                Debug.LogError("SET TO RIGHT " + GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite + " --- " + NTurnSprite + " " + LTurnSprite + " " + RTurnSprite);
            }

            notSelected = false;
            endGameCounter = endGameCounter + 1;
            Debug.Log("ENDGAME COUNTER IS ******************************************************" +
                "*************************************************** " + endGameCounter);
            if(endGameCounter >= 3)
            {
                Debug.Log("GAME OVER!!!");
                endGameCounter = 0;
                //Invoke("EndGame", 1);
                SceneManager.LoadScene(3);
            }

        }
        
    }

    public static void EndGame()
    {
        SceneManager.LoadScene(3);
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
                    arrows[i].GetComponent<MeshRenderer>().enabled = false;
                    arrows.RemoveAt(i);

                    GameObject.Find("DirectionDisplay").GetComponent<Image>().sprite = NTurnSprite;

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