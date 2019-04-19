using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int movementSpeed;
	public int rotateSpeed;
	public int breakSensitivity;


	void Start()
	{

	}

	void Update()
	{

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.down, Input.GetAxis("Horizontal") * -1 * rotateSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(0, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0, 0, -0.07f);
		}


		// Steering Right
		/*if ( Input.GetAxis("Horizontal") > 0 )
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        }
        // Steering Left
        if ( Input.GetAxis("Horizontal") < 0 )
        {
            transform.Rotate(Vector3.down, Input.GetAxis("Horizontal") * -1 * rotateSpeed, 0);
        }
        // Gas Pedal
        if ( Input.GetAxis("Vertical") != -1 && Input.GetAxis("Vertical") != 0 )
        {
            float tmp2 = (Input.GetAxis("Vertical") + 1.0f) * movementSpeed;
            Debug.Log(tmp2);
            float tmp = 5;
            transform.Translate(0, 0, tmp2 * movementSpeed);
        }
        // Break pedal
        if ( Input.GetAxis("Mouse ScrollWheel") != 0 && Input.GetAxis("Mouse ScrollWheel") != 0.1f )
        {
            //transform.Translate(0, 0, )
        }*/

	}
}
