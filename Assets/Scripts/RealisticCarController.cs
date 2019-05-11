using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealisticCarController : MonoBehaviour
{
    public Rigidbody car;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    public int Brakes;


    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }


    public void GetInput()
    {

        /* STEERING INPUT DETECTION */
        m_horizontalInput = Input.GetAxis("Horizontal");


        //Decelerate if the vertical input isn't pressed
        if (Input.GetAxis("Vertical") == -1 && !Input.GetKey("joystick 1 button 4") && ((((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) <= 0))
        {
            Brakes = 0;
            m_verticalInput = 8;
        }

        // REVERSAL CURRENTLY BROKEN
        else if (Input.GetKey("joystick 1 button 4"))
        {
            //Debug.Log("Reversal Applied!");
            m_verticalInput = -20;
        }

        /* BRAKING INPUT DETECTION */
        else if (((((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) > 0))
        {
            int tempBrake = (int)(((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) * 12;
            if (tempBrake > 25)
            {
                Brakes = (int)(((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) * 12;
                //Debug.Log("Brakes Applied!");
            }
        }

        /* GAS INPUT DETECTION */
        else if (((Input.GetAxis("Vertical") + 1) * 50) > 0)
        {
            Brakes = 0;
            m_verticalInput = ((Input.GetAxis("Vertical") + 1) * 50);
        }
    }


    private void Steer()
    {

        //Debug.Log("Steering");
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;

    }

    private void Accelerate()
    {

        //Debug.Log("Accelerating forwards!");
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
        Debug.Log("Checking m_verticalInput in accelerate func: " + m_verticalInput);
        Debug.Log("Checking frontW_motorTorque in accelerate func: " + frontDriverW.motorTorque);

        /*else if (m_verticalInput < 0)
        {
        {
            Debug.Log("Accelerating backwards!");

            frontDriverW.motorTorque = m_verticalInput * motorForce * Time.deltaTime;
            frontPassengerW.motorTorque = m_verticalInput * motorForce * Time.deltaTime;
            rearDriverW.motorTorque = m_verticalInput * motorForce * Time.deltaTime;
            rearPassengerW.motorTorque = m_verticalInput * motorForce * Time.deltaTime;
            Debug.Log("Checking motorForce: " + motorForce);
            Debug.Log("Checking frontW_motorTorque: " + frontDriverW.motorTorque);
        }*/
    }

    private void Braking()
    {
        Debug.Log("In braking func(), brakes are: " + Brakes);
        frontDriverW.brakeTorque = Brakes;
        rearDriverW.brakeTorque = Brakes;
        frontPassengerW.brakeTorque = Brakes;
        rearPassengerW.brakeTorque = Brakes;

    }

    private void UpdateWheelPoses()
    {
        //Debug.Log("Update Wheel Poses");
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void DetectInput()
    {
        Debug.Log(("Brakes are pressed at " + ((((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) + "%")));
        Debug.Log(("Gas is pressed at " + (Input.GetAxis("Vertical") + 1) * 50) + "%");
    }

	public double getSpeedMPH(){
		Debug.Log("SPEED IS....... " + (car.velocity.magnitude * 2.237f) + " mph");
		return (car.velocity.magnitude * 2.237f);
	}

    private void FixedUpdate()
    {
        if (!((((Input.GetAxis("Mouse ScrollWheel")) * -500) + 50) == 50 && ((Input.GetAxis("Vertical") + 1) * 50) == 50))
        {            
            DetectInput();
            GetInput();
            Steer();
            Accelerate();
            Braking();
            UpdateWheelPoses();
			getSpeedMPH();
        }
    }
}