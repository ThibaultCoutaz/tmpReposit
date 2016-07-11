using UnityEngine;
using System.Collections;

public class DashManager : MonoBehaviour {

    public enum DashState
    {
        Ready,
        Charging,
        Cooldown
    }

    public bool dashMouse = true;
    private bool canDashClick = false;
    public Vector2 minMaxPowerDashFowardClick = new Vector2(50, 100);
    private float CurrentPowerDashFowardClick;
    public float powerDashFowardKey = 100;
    public float powerDashSize = 50;
    public float DashCooldown = 10;
    
    public Camera pCamera;
    public GameObject pivotTrailRenderer; // temporaire a voir avec nouveau model.

    private PlayerScript ps;
    private Rigidbody rigb;

    // Use this for initialization
    void Start ()
    {
        rigb = GetComponent<Rigidbody>();
        ps = GetComponent<PlayerScript>();

        CurrentPowerDashFowardClick = minMaxPowerDashFowardClick.x;
        HUDManager.Instance.InitialiseDashBar(minMaxPowerDashFowardClick.y);
        HUDManager.Instance.HandleBarDashPower(CurrentPowerDashFowardClick);

        stateDR = DashState.Ready;
        stateDL = DashState.Ready;
    }
	
	void Update () {
        if (GetComponent<PlayerControl>().grounded)
        {
            ManageDashFoward();

            ManageDashLeft();

            ManageDashRight();
        }
    }
         /////////////////////////////////////////////////////////////////
        //FAIRE PREVISUALISATION DUDASH AVANT SERAIT TROP COOL TA MERE //
       /////////////////////////////////////////////////////////////////
    private float currentCooldownDF = 0;
    private DashState stateDF;
    private void ManageDashFoward()
    {
        switch (stateDF)
        {
            case DashState.Ready:
                if (dashMouse)
                {
                    if (InputManager.Instance.IsDashingFowardClick)
                    {
                        if (CurrentPowerDashFowardClick < minMaxPowerDashFowardClick.y)
                        {
                            CurrentPowerDashFowardClick++;
                            HUDManager.Instance.HandleBarDashPower(CurrentPowerDashFowardClick);
                        }
                        canDashClick = true;
                    }
                    else if (canDashClick)
                    {
                        rigb.AddForce(pCamera.transform.forward * CurrentPowerDashFowardClick, ForceMode.VelocityChange);
                        CurrentPowerDashFowardClick = minMaxPowerDashFowardClick.x;
                        HUDManager.Instance.HandleBarDashPower(CurrentPowerDashFowardClick);
                        canDashClick = false;
                        HUDManager.Instance.AddStateDisplay(DashCooldown,HUDListState.typeState.DashFoward);
                        stateDF = DashState.Cooldown;
                    }
                }
                else
                {
                    if (InputManager.Instance.IsDashingFowardKey)
                    {
                        rigb.AddForce(transform.forward * powerDashFowardKey, ForceMode.VelocityChange);
                        pivotTrailRenderer.SetActive(true);
                        ps.EditState(PlayerScript.stateCharacter.Dash);
                        Invoke("DiseableTrail", 0.5f);
                    }
                }
                break;
            case DashState.Cooldown:
                currentCooldownDF += Time.deltaTime;
                if (currentCooldownDF >= DashCooldown)
                {
                    stateDF = DashState.Ready;
                    currentCooldownDF = 0;
                }
                break;
        }
    }

    private float currentCooldownDR = 0;
    private DashState stateDR;
    private void ManageDashRight()
    {
        switch (stateDR)
        {
            case DashState.Ready:
                if (InputManager.Instance.IsDashingRight)
                {
                    rigb.AddForce(transform.right * powerDashSize, ForceMode.VelocityChange);
                    stateDR = DashState.Cooldown;
                    HUDManager.Instance.AddStateDisplay(DashCooldown, HUDListState.typeState.DashRight);
                }
                break;
            case DashState.Cooldown:
                currentCooldownDR += Time.deltaTime;
                if (currentCooldownDR >= DashCooldown)
                {
                    stateDR = DashState.Ready;
                    currentCooldownDR = 0;
                }
                break;
        }
    }

    private float currentCooldownDL = 0;
    private DashState stateDL;
    private void ManageDashLeft()
    {
        switch (stateDL)
        {
            case DashState.Ready:
                if (InputManager.Instance.IsDashingLeft)
                {
                    rigb.AddForce(-transform.right * powerDashSize, ForceMode.VelocityChange);
                    stateDL = DashState.Cooldown;
                    HUDManager.Instance.AddStateDisplay(DashCooldown, HUDListState.typeState.DashLeft);
                }
                break;
            case DashState.Cooldown:
                currentCooldownDL += Time.deltaTime;
                if (currentCooldownDL >= DashCooldown)
                {
                    stateDL = DashState.Ready;
                    currentCooldownDL = 0;
                }
                break;
        }
    }

    void DiseableTrail()
    {
        pivotTrailRenderer.SetActive(false);
        ps.EditState(PlayerScript.stateCharacter.Normal);
    }

}
