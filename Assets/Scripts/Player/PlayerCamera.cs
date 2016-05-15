using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCamera : CameraController
{
    public float MouseInfluenceX;
    public float MouseInfluenceY;
    public float MinY, MaxY;
    public float DeltaTimeRot;
    public float DeltaTimePos;
    public float SmoothTimePos;
    public float TargetPlayerRadius;
    public GameObject[] rendererPlayer;
    private Material[] playerMats;
    public Transform PlayerTransform;
    private bool Pause;
    public float MinDistFromPlayer = 1f;
    public float MaxDistFromPlayer = 5.0f;
    private float TimeSmoothForward = 0.5f;
    private float TimeSmoothBackward = 0.001f;
    public Transform Pivot;
    public Vector3 TargetDir { get; private set; }

    private Vector3 offsetPos;
    private Vector3 offsetUpCam;
    private Vector3 velocity = Vector3.zero;
    private InputManager inputs;
    private float deltaX, deltaY;
    private Vector3 target;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        List<Material> tmp_mats = new List<Material>();
        for (int i = 0; i < rendererPlayer.Length; i++)
        {
            foreach (Renderer renderer in rendererPlayer[i].GetComponents<Renderer>())
            {
                foreach (Material material in renderer.materials)
                {
                    tmp_mats.Add(material);
                }
            }
        }
        playerMats = tmp_mats.ToArray();
        offsetPos = transform.position - Pivot.position;
        offsetUpCam = Vector3.up * offsetPos.y;
        inputs = InputManager.Instance;
        Pause = false;
        deltaX = 0f;
        deltaY = 0f;
        target = Pivot.transform.position + Pivot.transform.forward * TargetPlayerRadius;
        TargetDir = Vector3.Normalize(target - Pivot.transform.position);
    }

    void MoveToPlayer()
    {
        Vector3 PlayerPosWithOffset = Pivot.position + offsetUpCam;
        Vector3 CP = PlayerPosWithOffset - transform.position;
        float CurrentDistFromPlayer = Vector3.Magnitude(CP);
        Vector3 normalizedCP = CP / CurrentDistFromPlayer;
        float deltaForward = Time.deltaTime * TimeSmoothForward;

        float deltaBackward = Time.deltaTime * TimeSmoothForward;

        float NextDistFromPlayer = 0.0f;
        float frustumHalfHeight = cam.nearClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumHalfWidth = frustumHalfHeight * cam.aspect;
        float distMaxRay = MaxDistFromPlayer;
        RaycastHit rch;
        if (Physics.BoxCast(PlayerPosWithOffset, new Vector3(frustumHalfWidth, frustumHalfHeight, 1f), -normalizedCP, out rch, cam.transform.rotation, distMaxRay))
        {

            NextDistFromPlayer = Mathf.Lerp(CurrentDistFromPlayer, rch.distance - 0.4f, deltaForward * Mathf.Exp(rch.distance));
        }
        else
        {
            NextDistFromPlayer = Mathf.Lerp(CurrentDistFromPlayer, MaxDistFromPlayer, deltaBackward * TimeSmoothBackward);
        }
        NextDistFromPlayer = Mathf.Max(NextDistFromPlayer, MinDistFromPlayer);
        NextDistFromPlayer = Mathf.Min(NextDistFromPlayer, MaxDistFromPlayer);

        Vector3 new_pos = PlayerPosWithOffset + NextDistFromPlayer * -normalizedCP;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                if (Vector3.Distance(new_pos, hit.point) < 0.4f)
                {
                    new_pos.y = hit.point.y + 0.4f;
                }
            }
        }

        transform.position = new_pos;
        offsetPos = transform.position - Pivot.position;
        float maxAlpha = 1.0f;
        float minAlpha = 0.3f;
        foreach (Material playerMat in playerMats)
        {
            Color newCol = playerMat.GetColor("_Color");
            newCol.a = (maxAlpha - minAlpha) * ((NextDistFromPlayer - MinDistFromPlayer) / (MaxDistFromPlayer - MinDistFromPlayer)) + minAlpha;
            playerMat.SetColor("_Color", newCol);
        }
    }

    protected override void Update()
    {
        if (!Pause)
        {
            float x = MouseInfluenceX * inputs.GetHorizontalMouse();
            float y = MouseInfluenceY * inputs.GetVerticalMouse();

            deltaX = x;

            // Check for target min/max height
            if ((y > 0 && (deltaY + y) < MaxY) || (y < 0 && (deltaY + y) > MinY))
                deltaY += y;

            if (deltaX > 360f)
                deltaX -= 360f;
            if (deltaX < -360f)
                deltaX += 360f;

            if (x == 0f) // Nothing, use same direction
                target = Pivot.transform.position + TargetDir * TargetPlayerRadius;
            else // Moving camera with mouse
            {
                // Rotate cam target around player
                target = Utils.RotatePointAround(Pivot.transform.position + TargetDir * TargetPlayerRadius, Pivot.position, new Vector3(0f, deltaX, 0f));
                // Calculate direction
                TargetDir = Vector3.Normalize(target - Pivot.transform.position);
            }

            // Translate cam
            Vector3 new_pos = Pivot.transform.position - Vector3.Normalize(target - Pivot.transform.position) * offsetPos.magnitude + offsetUpCam;
            new_pos = new Vector3(new_pos.x, new_pos.y - deltaY, new_pos.z);

            transform.position = Vector3.SmoothDamp(transform.position, new_pos, ref velocity, SmoothTimePos, Mathf.Infinity, Time.deltaTime * DeltaTimePos);

            // Y mouse pos
            target = new Vector3(target.x, target.y + deltaY, target.z);

            target += offsetUpCam;

            // Get the rotation
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

            // Smoothly rotate cam towards the target point
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * DeltaTimeRot);

            MoveToPlayer();
        }
        else
        {
            base.Update();
        }
    }

    public override void SetPause(bool p)
    {
        base.SetPause(p);
        Pause = p;
    }
}
