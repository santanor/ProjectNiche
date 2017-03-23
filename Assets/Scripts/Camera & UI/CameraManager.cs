using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class CameraManager : MonoBehaviour
{ 
    [Range(0f, 10f)]
    [SerializeField]
    private float tiltMax = 75f;                       // The maximum value of the x axis rotation of the pivot.
    [SerializeField]
    private float tiltMin = 45f;                       // The minimum value of the x axis rotation of the pivot.
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private Transform CameraRigAnchor;

    private float lookAngle;
    private float tiltAngle;

    private Vector3 m_PivotEulers;
    private Quaternion m_PivotTargetRot;
    private Quaternion m_TransformTargetRot;
    public Transform Target;
    [SerializeField]
    private float turnSpeed;

    void Start()
    {
        m_PivotEulers = Target.rotation.eulerAngles;

        m_PivotTargetRot = Target.transform.localRotation;
        m_TransformTargetRot = transform.localRotation;
    }


    protected void Update()
    {
        if (Input.GetMouseButton(2))
        {
            Cursor.visible = false;
            //HandleRotationMovement();
        }
        if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        FollowTarget();
        //HandleZoom();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    protected void FollowTarget()
    {
        transform.position = Target.position;
    }


    private void HandleRotationMovement()
    {
        // Read the user input
        var x = CrossPlatformInputManager.GetAxis("Mouse X");
        var y = CrossPlatformInputManager.GetAxis("Mouse Y");

        // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
        lookAngle += x * turnSpeed;

        // Rotate the rig (the root object) around Y axis only:
        m_TransformTargetRot = Quaternion.Euler(0f, lookAngle, 0f);

        // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
        tiltAngle -= y * turnSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);

        // Tilt input around X is applied to the pivot (the child of this object)
        m_PivotTargetRot = Quaternion.Euler(tiltAngle, m_PivotEulers.y, m_PivotEulers.z);
        pivot.localRotation = m_PivotTargetRot;
        CameraRigAnchor.transform.localRotation = m_TransformTargetRot;
        
    }
}
