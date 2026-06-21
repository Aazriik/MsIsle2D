using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerControls input;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchHoldAction;

    private void Awake()
    {
        input = GetComponent<PlayerControls>();

        touchPressAction = input.FindAction("TouchPress");
        touchPositionAction = input.FindAction("TouchPosition");
        touchHoldAction = input.FindAction("TouchHold");
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint
            (touchPositionAction.ReadValue<Vector2>());
    }
}
