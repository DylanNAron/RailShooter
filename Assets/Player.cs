using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In m/s")][SerializeField] float xSpeed = 8f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 8f;

    [SerializeField] float xRestraint = 10f;
    [SerializeField] float yRestraint = 10f;

    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float controlPitchFactor = -7.5f;

    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -20f;



    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();


    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToControl + pitchDueToPosition;
        float yaw=transform.localPosition.x * positionYawFactor;
        float roll=xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXpos = transform.localPosition.x + xOffset;
        float xPos = Mathf.Clamp(rawNewXpos, -xRestraint, xRestraint);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYpos = transform.localPosition.y + yOffset;
        float yPos = Mathf.Clamp(rawNewYpos, -yRestraint, yRestraint);


        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
