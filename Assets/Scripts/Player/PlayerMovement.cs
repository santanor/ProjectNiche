using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkMoveStopRadius = 0.2f;
    [SerializeField]    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    [SerializeField]    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool isDirectMode; //TODO make Static? 

    private void Start()
    {
        Assert.IsNotNull(cameraRaycaster);
        Assert.IsNotNull(m_Character);
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))//TODO allow player to map later or add to menu
        {
            isDirectMode = !isDirectMode;
            currentClickTarget = transform.position;//Clear click target
        }
            

        if (isDirectMode)
            ProcessDirectMovement();
        else
            ProcessMouseMovement();
    }

    private void ProcessDirectMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        var m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        var m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit " + cameraRaycaster.LayerHit);

            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;
                case Layer.Enemy:
                    break;
                default:
                    print("Unexpected layer");
                    return;
            }
        }

        var playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

