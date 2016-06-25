using System;

namespace Game
{
    //public class Propreties
    //{
    //    public float version = 0.1f;
    //    public float goldPerSecond = 10.0f;
    //}

    public enum Audio_Type
    {
        
    }

    public enum UI_Types
    {
        NULL,
        ConnectionInfos,
        TimerInGame,
        AmountGold,
        CharacterInfos,
        StateCharacter,
        Spell,
        Targeting,
        InfosTarget,
        GetBall,
        Debuging
    }

    public enum Lerp_Type
    {
        EaseOut,
        EaseIn,
        Exponential,
        Smoothstep,
        Smootherstep
    }
}
