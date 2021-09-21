using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputAsset", menuName = "GameInput")]
public class InputAsset : ScriptableObject, PlayerControls.IPlayerActions
{
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction runEvent = delegate { };
    public event UnityAction<bool> aimEvent = delegate { };
    public event UnityAction<bool> topDownCameraEvent = delegate { };
    public event UnityAction publish2dNawGoalEvent = delegate { };

    private PlayerControls playerControls;

    private bool aimButton;
    private bool topDownButton;
    private void OnEnable()
    {
        topDownButton = false;
        aimButton = false;
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Player.SetCallbacks(this);
        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            runEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnWasd(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            topDownButton = false;
            if (!aimButton)
            {
                aimButton = true;
                aimEvent.Invoke(true);
            }
            else
            {
                aimButton = false;
                aimEvent.Invoke(false);
            }
        }
    }

    public void OnTopDownCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!topDownButton)
            {
                topDownButton = true;
                topDownCameraEvent.Invoke(true);
            }
            else
            {
                topDownButton = false;
                topDownCameraEvent.Invoke(false);
            }
        }
    }
    public void OnPublish2dNawGoal(InputAction.CallbackContext context)
    {
        if (topDownButton == true)
        {
            publish2dNawGoalEvent.Invoke();
        }
    }
}
