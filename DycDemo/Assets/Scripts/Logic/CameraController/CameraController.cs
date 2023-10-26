using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Rendering.Universal;


//控制主场景相机的运动  先快速实现一个 后续要处理 ui相关的
public class CameraController : MonoSingleton<CameraController>
{
    private float panSpeed;
    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    Transform _cameraTransform;
    [SerializeField]
    Transform _pointTransform;
    [SerializeField]
    CinemachineVirtualCamera vc;

    CinemachineFramingTransposer cft;
    CinemachinePOV cpov;

    public CinemachineVirtualCamera v2;
    Camera mainCamera;
    public Camera uiCamera;

    public float miniDistance;
    public Transform CameraTransform => _cameraTransform;


    public Vector2 zoomBound = new Vector2(35, 90);
    public Vector2 zoomRotate = new Vector2(30, 65);
    public float zoomRotateSpeed = 10;
    public LayerMask groundMask;

    float preZoomPanSpeed;

#if UNITY_EDITOR
    float zoomPanSpeedSeed = 6f;
#else
    float zoomPanSpeedSeed = 4f;
#endif

    Vector3 _newPos;
    Vector3 _screenPos;
    Vector3 _moveDir;
    bool _isPress;
    Transform _cacheTransform;
    float _eacheRotateByZoom;
    bool ignoreMove;
    protected override void Awake()
    {
        base.Awake();
        _cacheTransform = transform;
        _isPress = false;
        _newPos = _cacheTransform.localPosition;
        cft = vc.GetCinemachineComponent<CinemachineFramingTransposer>();
        cpov = vc.GetCinemachineComponent<CinemachinePOV>();
        preZoomPanSpeed = zoomPanSpeedSeed / (zoomBound.y - zoomBound.x);
        ignoreMove = false;
        ReSetPanSpeed();

        _eacheRotateByZoom = (zoomRotate.y - zoomRotate.x) / (zoomBound.y - zoomBound.x);


        v2.gameObject.SetActive(false);
        mainCamera = _cameraTransform.gameObject.GetComponent<Camera>();


    }

    public void RegisterListenner()
    {
        //var gameMgr = GameManager.Instance;
        //PlayerInputManager.Instance.MouseMoveEvent += OnMoved;
        //PlayerInputManager.Instance.ZoomChangeEvent += OnZoomed;

        //var mgr = gameMgr.GetManager<GridBuildSystemManager>();
        //mgr.InOutMoveVisualBuildingEvent += OnInOutMoveVisualBuildinged;
    }


    //public bool ShowLookTourist(SimpleCharacterController target_)
    //{
    //    v2.transform.position = target_.LookTarget.position;
    //    v2.transform.LookAt(target_.transform.position + Vector3.up * 0.5f);
    //    v2.gameObject.SetActive(true);

    //    mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(Layers.TOURISTER_LAYER));
    //    mainCamera.cullingMask |= (1 << LayerMask.NameToLayer(Layers.LOOK_TOURISTER_LAYER));

    //    return true;
    //}



    public void ExitLookTourist()
    {
        v2.gameObject.SetActive(false);
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(Layers.LOOK_TOURISTER_LAYER));
        mainCamera.cullingMask |= (1 << LayerMask.NameToLayer(Layers.TOURISTER_LAYER));

    }

    private void Start()
    {
        ResetRotate();
    }

    private void OnInOutMoveVisualBuildinged(bool start_)
    {
        ignoreMove = start_;
    }

    private void OnDestroy()
    {
        //base.OnDestroy();
        //var gameMgr = GameManager.Instance;
        //if (gameMgr != null)
        //{
        //    var mgr = gameMgr.GetManager<GridBuildSystemManager>();
        //    mgr.InOutMoveVisualBuildingEvent -= OnInOutMoveVisualBuildinged;
        //}

        //var inputMgr = PlayerInputManager.Instance;
        //if (inputMgr != null)
        //{
        //    inputMgr.MouseMoveEvent -= OnMoved;
        //    inputMgr.ZoomChangeEvent -= OnZoomed;
        //}
    }

    private void OnZoomed(float scroll)
    {
        //LogUtil.LogToActionEvent(string.Format("Zoom  {0}", scroll));
        var curDistance = cft.m_CameraDistance;
        curDistance -= zoomSpeed * scroll * Time.deltaTime;

        cft.m_CameraDistance = Mathf.Clamp(curDistance, zoomBound.x, zoomBound.y);
        ReSetPanSpeed();
        ResetRotate();
    }

    private void OnMoved(Vector2 dir, bool ui_)
    {
        if (ignoreMove || ui_) return;
        _newPos = _pointTransform.localPosition;
        var _dir = dir.normalized;
        var _newDir = new Vector3(_dir.x, 0, _dir.y);
        Vector3 newVec = Quaternion.Euler(0, 45, 0) * _newDir;
        _newPos -= newVec * panSpeed * Time.deltaTime;
        _newPos.x = Mathf.Clamp(_newPos.x, -5, 5);
        _newPos.z = Mathf.Clamp(_newPos.z, -5, 5);
        _pointTransform.localPosition = _newPos;
    }

    //35   1    90  4
    void ReSetPanSpeed()
    {
#if UNITY_EDITOR
        panSpeed = (cft.m_CameraDistance - zoomBound.x) * preZoomPanSpeed + 1f;
#else
        panSpeed = (cft.m_CameraDistance - zoomBound.x) * preZoomPanSpeed + 1f ;
#endif
    }

    void ResetRotate()
    {
        //var localAngle = cft.transform.localEulerAngles;
        var f = (cft.m_CameraDistance - zoomBound.x) * _eacheRotateByZoom;

        _targetVA = f + zoomRotate.x;
        //cpov.m_VerticalAxis.Value = f + 30;
        //cft.transform.localEulerAngles = new Vector3((cft.m_CameraDistance - zoomBound.x) * _eacheRotateByZoom + 30, 45, 0);
    }
    float _targetVA;

    private void Update()
    {
        if (_targetVA != 0 && cpov.m_VerticalAxis.Value != _targetVA)
        {
            cpov.m_VerticalAxis.Value = Mathf.SmoothStep(cpov.m_VerticalAxis.Value, _targetVA, Time.deltaTime * zoomRotateSpeed);
        }
    }

    //public void RegisteMainCamerFollowPoint(GameObject point)
    //{
    //    _pointTransform = point.transform;
    //    vc.Follow = point.transform;
    //}

    public void RegisteOverrideCamera(Camera uiCamera)
    {
        var data = mainCamera.GetUniversalAdditionalCameraData();
        data.cameraStack.Add(uiCamera);
    }
}
