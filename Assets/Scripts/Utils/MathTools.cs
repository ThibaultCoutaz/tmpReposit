using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MathTools
{
    delegate float LerpFunction(float t);

    private static Dictionary<Game.Lerp_Type, LerpFunction> lerpFunctionsList = new Dictionary<Game.Lerp_Type, LerpFunction>
    {
        { Game.Lerp_Type.EaseIn, LerpEaseIn },
        { Game.Lerp_Type.EaseOut, LerpEaseOut },
        { Game.Lerp_Type.Exponential, LerpExpo },
        { Game.Lerp_Type.Smoothstep, LerpSmooth },
        { Game.Lerp_Type.Smootherstep, LerpSmoother }
    };

    public static float LerpEaseIn(float t)
    {
        return Mathf.Clamp01(1f - Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static float LerpEaseOut(float t)
    {
        return Mathf.Clamp01(Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static float LerpExpo(float t)
    {
        return Mathf.Clamp01(t * t);
    }

    public static float LerpSmooth(float t)
    {
        return Mathf.Clamp01(t * t * (3f - 2f * t));
    }

    public static float LerpSmoother(float t)
    {
        return Mathf.Clamp01(t * t * t * (t * (6f * t - 15f) + 10f));
    }

    public static float LerpInvoke(Game.Lerp_Type type, float t)
    {
        return lerpFunctionsList[type].Invoke(t);
    }
}

