using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


[DefaultExecutionOrder(-100)] // Ensure this runs before most other scripts
public class InputManager : MonoBehaviour
{
    // Make this a Singleton.
    public static InputManager Instance;

    #region Events
    // Public facing events and methods for other scripts to subscribe to or call.
    // On Start Touch Event.
    //public delegate void StartTouchEvent(Vector2 position, float time);
    //public event StartTouchEvent OnStartTouch;
    //// On End Touch Event.
    //public delegate void EndTouchEvent(Vector2 position, float time);
    //public event EndTouchEvent OnEndTouch;

    //// On Start Touch Hold Event.
    //public delegate void StartTouchHoldEvent(Vector2 position, float time);
    //public event StartTouchHoldEvent OnStartTouchHold;
    //// On End Touch Hold Event.
    //public delegate void EndTouchHoldEvent(Vector2 position, float time);
    //public event EndTouchHoldEvent OnEndTouchHold;

    #endregion

    public delegate void OnTouchBeginEvent(Vector2 position, float time);
    public event System.Action OnTouchBegin;

    public delegate void OnTouchEndEvent(Vector2 position, float time);
    public event System.Action OnTouchEnd;
    //public event System.Action<Vector3> OnPhoneTilt;

    // Getting Touch Screen Position.
    public Vector2 GetTouchScreenPosition() => input.Touch.TouchPosition.ReadValue<Vector2>();
    // Getting Touch WORLD Position.
    public Vector3 GetTouchWorldPosition(Camera mainCamera = null)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;


        Vector2 screenPos = GetTouchScreenPosition();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane));

        return worldPos;
    }

    //public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    //{
    //    position.x = camera.nearClipPlane;
    //    return camera.ScreenToWorldPoint(position);
    //}

    // Input Systems Game Object in Project Folder.
    private PlayerControls input;

    void Awake()
    {
        // Singleton. If there is no instance of this object in the scene, make one.
        // If there's already one there, Destroy. If there isn't, create one.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Input is now tied to the Player Controls Input Actions.
        input = new PlayerControls();
    }

    #region Enable/Disable
    private void OnEnable()
    {
        // Enabling the PlayerControls Input Actions.
        input.Enable();
        // Enabling Touch Simulation through InputSystem.EnhancedTouch Namespace rather than through the Debugger.
        TouchSimulation.Enable();

        // Unity Engine InputSystem.EnhancedTouch.
        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;

        input.Touch.TouchPress.started += ctx => OnTouchBegin?.Invoke();
        input.Touch.TouchPress.canceled += ctx => OnTouchEnd?.Invoke();
        //input.Touch.Tilt.performed += ctx => OnPhoneTilt?.Invoke(ctx.ReadValue<Vector3>());
    }

    private void OnDisable()
    {
        // Disabling the PlayerControls Input Actions.
        input.Disable();
        TouchSimulation.Disable();

        input.Touch.TouchPress.started -= ctx => OnTouchBegin?.Invoke();
        input.Touch.TouchPress.canceled -= ctx => OnTouchEnd?.Invoke();

        // Unity Engine InputSystem.EnhancedTouch.
        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    #endregion


    // NEW STUFF
    private void Start()
    {
        // Unity Engine InputSystem.
        // Touch Press.
        //input.Touch.TouchPress.started += ctx => StartTouch(ctx);
        //input.Touch.TouchPress.canceled += ctx => EndTouch(ctx);

        // Touch Hold.
        //input.Touch.TouchHold.started += ctx => StartTouchHold(ctx);
        //input.Touch.TouchHold.canceled += ctx => EndTouchHold(ctx);
    }




    // UnityEngine.InputSystem
    //private void StartTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Touch started " + input.Touch.TouchPosition.ReadValue<Vector2>());
    //    if (OnStartTouch != null)
    //        OnStartTouch(input.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    //}

    //// UnityEngine.InputSystem
    //private void EndTouch(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Touch ended " + input.Touch.TouchPosition.ReadValue<Vector2>());
    //    if (OnEndTouch != null)
    //        OnEndTouch(input.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    //}




    //private void FingerDown(Finger finger)
    //{
    //    if (OnStartTouch != null)
    //        OnStartTouch(finger.screenPosition, Time.time);
    //}



    //// UnityEngine.InputSystem
    //private void StartTouchHold(InputAction.CallbackContext context)
    //{
    //    if (OnStartTouchHold != null)
    //        OnStartTouchHold(input.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    //}

    //// UnityEngine.InputSystem
    //private void EndTouchHold(InputAction.CallbackContext context)
    //{
    //    if (OnEndTouchHold != null)
    //        OnEndTouchHold(input.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    //}


    private void Update()
    {
        //Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);

        //foreach(UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        //{
        //    Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
        //}
    }
}
