using UnityEngine;
using System.Collections;
using Game;

public class CameraController : MonoBehaviour
{
    public bool CamMoving { get; private set; }

    public float TimeTraveling;
    public float TimeZoom;
    public float TimeRotate;
    public float TimeLook;
    public float TimeClip;

    public Lerp_Type LerpTypeTraveling;
    public Lerp_Type LerpTypeZoom;
    public Lerp_Type LerpTypeRotate;
    public Lerp_Type LerpTypeLook;
    public Lerp_Type LerpTypeClip;

    // Traveling
    private bool traveling;
    private Vector3 targetTravel;
    private Transform lookAtTravel;
    private float currentTimeTraveling;

    // Rotation
    private bool rotation;
    private Vector3 pivotRot;
    private Vector3 targetAngles;
    private Vector3 currentAngles;
    private Vector3 baseAngle;
    private Transform lookAt;
    private float currentTimeRotation;

    // Zoom
    private const float MinFov = 5f;
    private float maxFov;
    private float targetFov;
    private bool zooming;
    private float currentTimeZoom;

    // Look at
    private Vector3 targetLookAt;
    private bool looking;
    private float currentTimeLookAt;

    // Clipping
    private bool clipping;
    private float targetNear;
    private float targetFar;
    private float currentTimeClipping;

    protected Camera cam;

    public delegate void ActionOverDelegate(bool over);
    public ActionOverDelegate overDelegate;

    public void ActionOver()
    {
        if (overDelegate != null)
        {
            overDelegate(!(looking && zooming && traveling && rotation && clipping));
        }
    }

    // Use this for initialization
    protected virtual void Awake()
    {
        cam = GetComponent<Camera>();
        maxFov = cam.fieldOfView - MinFov;
        looking = false;
        zooming = false;
        traveling = false;
        rotation = false;
        clipping = false;
        CamMoving = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (traveling)
        {
            float t = currentTimeTraveling / TimeTraveling;

            if (t <= 1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetTravel, MathTools.LerpInvoke(LerpTypeTraveling, t));

                if (lookAtTravel != null)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(lookAtTravel.position - transform.position),
                        MathTools.LerpInvoke(LerpTypeLook, t));
                }

                currentTimeTraveling += Time.deltaTime;
            }
            else
            {
                lookAtTravel = null;
                traveling = false;
                CamMoving = false;
                ActionOver();
            }
        }

        if (zooming)
        {
            float t = currentTimeZoom / TimeZoom;

            if (t <= 1f)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, MathTools.LerpInvoke(LerpTypeZoom, t));
                currentTimeZoom += Time.deltaTime;
            }
            else
            {
                zooming = false;
                ActionOver();
            }
        }

        if (rotation)
        {
            float t = currentTimeRotation / TimeRotate;

            if (t <= 1f)
            {
                Vector3 angles = Vector3.Lerp(baseAngle, targetAngles, MathTools.LerpInvoke(LerpTypeRotate, t));
                transform.position = Utils.RotatePointAround(transform.position, pivotRot, angles - currentAngles);
                currentAngles = angles;
                transform.LookAt(lookAt);
                currentTimeRotation += Time.deltaTime;
            }
            else
            {
                rotation = false;
                ActionOver();
            }
        }

        if (looking)
        {
            float t = currentTimeLookAt / TimeLook;

            if (t <= 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetLookAt - (traveling ? targetTravel : transform.position)),
                    MathTools.LerpInvoke(LerpTypeLook, t));
                currentTimeLookAt += Time.deltaTime;
            }
            else
            {
                looking = false;
                ActionOver();
            }
        }

        if (clipping)
        {
            float t = currentTimeClipping / TimeClip;

            if (t <= 1f)
            {
                float u = MathTools.LerpInvoke(LerpTypeClip, t);
                cam.nearClipPlane = Mathf.Lerp(cam.nearClipPlane, targetNear, u);
                cam.farClipPlane = Mathf.Lerp(cam.farClipPlane, targetFar, u);
                currentTimeClipping += Time.deltaTime;
            }
            else
            {
                clipping = false;
                ActionOver();
            }
        }
    }

    public void Clip(float near, float far)
    {
        currentTimeClipping = 0f;
        targetNear = near;
        targetFar = far;
        clipping = true;
    }

    public virtual void SetPause(bool p)
    {
        if (!p)
        {
            looking = false;
            zooming = false;
            traveling = false;
            rotation = false;
            CamMoving = false;
        }
    }

    public void Traveling(Vector3 from, Vector3 to, Transform look_at)
    {
        Traveling(from, to);
        currentTimeLookAt = 0f;
        lookAtTravel = look_at;
    }

    public void Traveling(Vector3 to, Transform look_at)
    {
        Traveling(to);
        currentTimeLookAt = 0f;
        lookAtTravel = look_at;
    }

    public void Traveling(Vector3 from, Vector3 to)
    {
        transform.position = from;
        Traveling(to);
    }

    public void Traveling(Vector3 to)
    {
        currentTimeTraveling = 0f;
        targetTravel = to;
        lookAtTravel = null;
        traveling = true;
        CamMoving = true;
    }

    public void Zoom(float zoom)
    {
        currentTimeZoom = 0f;
        targetFov = MinFov + maxFov * (1f - Mathf.Clamp01(zoom));
        zooming = true;
    }

    public void RotateCam(Transform pivot, Vector3 angles)
    {
        currentTimeRotation = 0f;
        pivotRot = pivot.position;
        targetAngles = angles;
        baseAngle = transform.rotation.eulerAngles;
        currentAngles = Vector3.zero;
        lookAt = pivot;
        rotation = true;
    }

    public void LookAtTarget(Vector3 target)
    {
        if (lookAtTravel == null)
        {
            currentTimeLookAt = 0f;
            targetLookAt = target;
            looking = true;
        }
        else
            throw new UnityException("Can't look at when already looking");
    }
}

