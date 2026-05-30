using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension{

    public static bool Lessthen(this Vector3 vec1, Vector3 vec2)
    {
        if (vec1.x < vec2.x && vec1.y < vec2.y)
            return true;
        return false;
    }

    public static bool Greaterthen(this Vector3 vec1, Vector3 vec2)
    {
        if (vec1.x > vec2.x && vec1.y > vec2.y)
            return true;
        return false;
    }

    public static Vector2 Angle2Direcrion(this Vector3 bullet, float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vector2 Angle2Direcrion(this float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vector2 Unit2UnitDirection(this Vector3 source, Vector3 target)
    {
        return (target - source).normalized;
    }

    //public static Vector2 rotateAroundPoint(this Vector3 vec, Vector3 point, float rotation)
    //{

    //}

    public static float Angle(this Vector3 source, Vector2 to)
    {
        return Mathf.Atan2(to.y - source.y, to.x - source.x) * Mathf.Rad2Deg;
    }

    public static Vector3 Floor(this Vector3 source)
    {
        return new Vector3(Mathf.Floor(source.x), Mathf.Floor(source.y), Mathf.Floor(source.z));
    }

    public static UnitAnimator.AnimationTypes Direction2AnimationType(this Vector3 source)
    {
        if (-0.25f < source.x && source.x < 0.25f && 0f < source.y) 
            return UnitAnimator.AnimationTypes.U_MoveAnimation;

        if (-0.25f < source.x && source.x < 0.25f && source.y < 0f) 
            return UnitAnimator.AnimationTypes.D_MoveAnimation;

        if (-0.25f < source.y && source.y < 0.25f && 0f < source.x) 
            return UnitAnimator.AnimationTypes.R_MoveAnimation;

        if (-0.25f < source.y && source.y < 0.25f && source.x < 0f) 
            return UnitAnimator.AnimationTypes.L_MoveAnimation;

        if (0f < source.x && 0f < source.y)
            return UnitAnimator.AnimationTypes.UR_MoveAnimation;

        if (source.x < 0f && 0f < source.y) 
            return UnitAnimator.AnimationTypes.UL_MoveAnimation;

        if (0f < source.x && source.y < 0f) 
            return UnitAnimator.AnimationTypes.DR_MoveAnimation;

        if (source.x < 0f && source.y < 0f) 
            return UnitAnimator.AnimationTypes.DL_MoveAnimation;

        return UnitAnimator.AnimationTypes.Idle;
    }

    public static UnitAnimator.AnimationTypes AnimationTypeChange(this UnitAnimator.AnimationTypes source)
    {

        if (source == UnitAnimator.AnimationTypes.R_MoveAnimation)
            return UnitAnimator.AnimationTypes.DR_MoveAnimation;

        if (source == UnitAnimator.AnimationTypes.L_MoveAnimation)
            return UnitAnimator.AnimationTypes.DL_MoveAnimation;

        if (source == UnitAnimator.AnimationTypes.U_MoveAnimation)
            return UnitAnimator.AnimationTypes.UR_MoveAnimation;

        if (source == UnitAnimator.AnimationTypes.D_MoveAnimation)
            return UnitAnimator.AnimationTypes.DR_MoveAnimation;

        return source;
    }
}
