using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInterface : MonoBehaviour
{
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    
    public UnityAction<ClickData> SendClickData;
    public GameInputsSO controls;
    private ClickData _clickData;
    private Camera _cameraMain;
    private CameraUtility _cameraUtility;
    private Vector2 _clickPosition;
    private Vector3 _mouseWorldPosition;
    private bool _isHolding;

    public GameObject prefabObj;
    private GameObject _clone;
    
    private void Awake()
    {
        _clickData = ScriptableObject.CreateInstance<ClickData>();
        _cameraUtility = ScriptableObject.CreateInstance<CameraUtility>();
        _cameraMain = Camera.main;
        controls.GameInputsObj.DefaultControls.PrimaryPress.performed += OnClick;
        controls.GameInputsObj.DefaultControls.PrimaryPress.canceled += OffClick;
        controls.GameInputsObj.DefaultControls.PrimaryPosition.performed += context => _clickPosition = context.ReadValue<Vector2>();
    }
    
    private void OnEnable()
    {
        controls.GameInputsObj.DefaultControls.PrimaryPress.Enable();
    }
    
    private void OnDisable()
    {
        controls.GameInputsObj.DefaultControls.PrimaryPress.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        _isHolding = true;
        controls.GameInputsObj.DefaultControls.PrimaryPosition.Enable();
        _clickData.positionStart = _clickPosition;
        Debug.Log("CAM POSITION" + GetCamPosition());
        Debug.Log("HIT POSITION" + GetHitPointPosition());
        _clone = Instantiate(prefabObj, GetHitPointPosition(), Quaternion.identity);
        
        StartCoroutine(UpdateMousePosition());
    }
    
    private IEnumerator UpdateMousePosition()
    {
        while (_isHolding)
        {
            _clickData.positionCurrent = _clickPosition;
            yield return _waitForFixedUpdate;
        }
    }


    private void OffClick(InputAction.CallbackContext context)
    {
        _isHolding = false;
        controls.GameInputsObj.DefaultControls.PrimaryPosition.Disable();
        _clickData.positionEnd = _clickData.positionCurrent;
        //SendClickData(_clickData);
    }

    private Vector3 GetCamPosition()
    {
        Vector3 camPosition = _cameraUtility.ScreenToWorld(_cameraMain, _clickPosition);
        return camPosition;
    }

    private Vector3 GetHitPointPosition()
    {
        var hitPosition = _cameraUtility.ScreenPointToRay(_cameraMain, GetCamPosition());
        return hitPosition;
    }
    
    
/*    
    private Vector2 move, movement;
    public FloatData speed;
    private void Awake()
    {
        controls.GameInputsObj.KeyActionMap.Vertical.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GameInputsObj.KeyActionMap.Vertical.canceled += ctx => move = Vector2.zero;
    }
 
    private void OnEnable()
    {
        controls.GameInputsObj.KeyActionMap.Enable();
    }
    private void OnDisable()
    {
        controls.GameInputsObj.KeyActionMap.Disable();
    }
    
    private void FixedUpdate()
    {
        movement.Set(move.x, move.y);
        movement *= speed.value * UnityEngine.Time.deltaTime;
        transform.Translate(movement, Space.World);
    }



using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-5)]
public class TouchSwipeBehaviour : MonoBehaviour
{
    public UnityAction<FromTouchData> sendTouchData;
    public GameInputsSO controls;
    public float minimumDistance = .2f, maximumTime = 1f;
    private FromTouchData touchData;
    private Camera cameraMain;
    private CameraUtility cameraUtility;
    
    private void Awake()
    {
        touchData = ScriptableObject.CreateInstance<FromTouchData>();
        cameraMain = Camera.main;
        cameraUtility = ScriptableObject.CreateInstance<CameraUtility>();
        controls.GameInputsObj.Touch.PrimaryContact.started += StartTouchPrimary;
        controls.GameInputsObj.Touch.PrimaryContact.canceled += EndTouchPrimary;
    }
    private void OnEnable()
    {
        controls.GameInputsObj.Touch.Enable();
    }
    private void OnDisable()
    {
        controls.GameInputsObj.Touch.Enable();
    }
    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        touchData.positionStart = GetCtx(ctx);
        touchData.timeStart = (float)ctx.startTime;
    }
    
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        touchData.positionEnd = GetCtx(ctx);
        touchData.timeEnd = (float)ctx.time;
        GetSwipeDirectionAndTime();
        sendTouchData(touchData);
    }
    private Vector3 GetCtx(InputAction.CallbackContext ctx)
    {
        var camPosition = cameraUtility.ScreenToWorld(cameraMain,
            controls.GameInputsObj.Touch.PrimaryPositition.ReadValue<Vector2>());
        return camPosition;
    }
    private void GetSwipeDirectionAndTime()
    {
        if (!(Vector3.Distance(touchData.positionStart, touchData.positionEnd) >= minimumDistance) ||
            !((touchData.timeEnd - touchData.timeStart) <= maximumTime)) return;
        var vectorDir = touchData.positionEnd - touchData.positionStart;
        touchData.direction = new Vector2(vectorDir.x, vectorDir.y);
        touchData.force = touchData.timeEnd - touchData.timeStart;
    }
}

*/
}
