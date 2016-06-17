using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    //protected InputManager() { }

    public static InputManager Instance = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool IsRunning { get; private set; }
    public bool IsWalking { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsPassing { get; private set; }
    public bool IsCancelling { get; private set; }
    public bool IsInGodMode { get; private set; }
    public bool IsDashingFoward { get; private set; }
    public bool IsDashingRight { get; private set; }
    public bool IsDashingLeft { get; private set; }
    public bool IsUsingSpell { get; private set; }
    public bool IsSpellA { get; private set; }
    public bool IsSpellE { get; private set; }
    public bool IsSpellR { get; private set; }

    private float cdButton = 0.1f;
    private int countButtonFoward = 0;
    private int countButtonRight = 0;
    private int countButtonLeft = 0;

    void Update()
    {
        
        IsWalking = (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0);
        IsJumping = Input.GetButtonDown("Jump");
        IsPassing = Input.GetButtonDown("Pass");
        IsUsingSpell = Input.GetButtonDown("UseSpell");
        IsSpellA = Input.GetButton("SpellA");
        IsSpellE = Input.GetButton("SpellE");
        IsSpellR = Input.GetButton("SpellR");

        IsDashingFoward = false;
        IsDashingRight = false;
        IsDashingLeft = false;

        if (Input.GetButtonDown("DashFoward"))
        {
            if(cdButton > 0 && countButtonFoward == 1)
            {
                IsDashingFoward = true;
            }
            else
            {
                cdButton = 0.2f;
                countButtonFoward += 1;
            }
        }

        if (Input.GetButtonDown("DashRight"))
        {
            if (cdButton > 0 && countButtonRight == 1)
            {
                IsDashingRight = true;
            }
            else
            {
                cdButton = 0.2f;
                countButtonRight += 1;
            }
        }

        if (Input.GetButtonDown("DashLeft"))
        {
            if (cdButton > 0 && countButtonLeft == 1)
            {
                IsDashingLeft = true;
            }
            else
            {
                cdButton = 0.5f;
                countButtonLeft += 1;
            }
        }

        if (cdButton > 0)
        {
            cdButton -= 1 * Time.deltaTime;
        }
        else
        {
            countButtonFoward = 0;
            countButtonLeft = 0;
            countButtonRight = 0;
        }

        IsCancelling = Input.GetButtonDown("Cancel");

        IsInGodMode = Input.GetKeyDown(KeyCode.G);
        
    }

    public float GetHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetHorizontalMouse()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetVerticalMouse()
    {
        return Input.GetAxis("Mouse Y");
    }
}
