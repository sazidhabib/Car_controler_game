using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float CurrentBreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider ForntLeftWheelCollider;
    [SerializeField] private WheelCollider ForntRightWheelCollider;
    [SerializeField] private WheelCollider RearLeftWheelCollider;
    [SerializeField] private WheelCollider RearRightWheelCollider;
    
    [SerializeField] private Transform ForntLeftWheelTransform;
    [SerializeField] private Transform ForntRightWheelTransform;
    [SerializeField] private Transform RearLeftWheelTransform;
    [SerializeField] private Transform RearRightWheelTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(ForntLeftWheelCollider, ForntLeftWheelTransform);
        UpdateSingleWheel(ForntRightWheelCollider, ForntRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);

    }

    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 Pose;
        Quaternion rot;
        WheelCollider.GetWorldPose(out Pose, out rot);
        WheelTransform.rotation = rot;
        WheelTransform.position = Pose;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        ForntLeftWheelCollider.steerAngle = currentSteerAngle;
        ForntRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void HandleMotor()
    {
        ForntLeftWheelCollider.motorTorque = verticalInput * motorForce;
        ForntRightWheelCollider.motorTorque = verticalInput * motorForce;
        CurrentBreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking) 
        {
            ApplyBreaking(CurrentBreakForce);
        
        }
        else
        {
            ApplyBreaking(0);
        }
        
    }

    private void ApplyBreaking( float CurrentBreakForce)
    {
        
        ForntLeftWheelCollider.brakeTorque = CurrentBreakForce;
        ForntRightWheelCollider.brakeTorque = CurrentBreakForce;
        RearLeftWheelCollider.brakeTorque = CurrentBreakForce;
        RearRightWheelCollider.brakeTorque = CurrentBreakForce;
        
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}
