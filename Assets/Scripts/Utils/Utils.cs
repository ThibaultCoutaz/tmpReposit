using UnityEngine;
using System.Collections;

public static class Utils
{

    public const float NONE = -100.0f;
    public static readonly Vector3 NONE_VEC3 = new Vector3(NONE, NONE, NONE);

    public static Vector3 RandVectorInRange(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }

    public static Vector3 RandVectorInRange2D(float min, float max)
    {
        return new Vector3(Random.Range(min, max), 0.0f, Random.Range(min, max));
    }

    public static Vector3 RandVectorInRangeXZ2D(float minX, float maxX, float minZ, float maxZ)
    {
        return new Vector3(Random.Range(minX, maxX), 0.0f, Random.Range(minZ, maxZ));
    }

    public static Vector3 RandPointInCircle(Vector3 origin, float ray, int maxTry)
    {
        for (int i = 0; i < maxTry; i++)
        {
            Vector3 delta = RandVectorInRange2D(-ray, ray);
            if (delta.magnitude < ray)
                return origin + delta;
        }
        return origin;
    }

    public static Vector3 RandPointOnCircle(Vector3 origin, float ray, int maxTry)
    {
        for (int i = 0; i < maxTry; i++)
        {
            Vector3 delta = RandVectorInRange2D(-ray, ray);
            if (Mathf.Abs(delta.magnitude - ray) <= 0.01)
                return origin + delta;
        }
        return origin;
    }

    public static Vector3 RotatePointAround(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        // Get point direction relative to pivot
        Vector3 dir = point - pivot;
        // Rotate it
        dir = Quaternion.Euler(angles) * dir;
        return dir + pivot;
    }

    /// <summary>
    /// Permet de changer le mode du shader standard
    /// </summary>
    /// <param name="material">le material à utiliser</param>
    /// <param name="blendMode"> le mode voulu : Opaque , Cutout, Fade, Transparent</param>
    public static void SetupMaterialWithBlendMode(Material material, string blendMode)
    {
        switch (blendMode)
        {
            case "Opaque":
                material.SetFloat("_Mode", 0);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case "Cutout":
                material.SetFloat("_Mode", 1);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case "Fade":
                material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case "Transparent":
                material.SetFloat("_Mode", 3);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }

    public static string SecondToString(float x)
    {
        int a, b, c, d, e;
        return ((a = (int)(x * 0.00000165343f)) > 0 ? a + " s " : "")
            + ((b = (int)((x -= a * 604800) * 0.00001157407f)) + a > 0 ? b + " j " : "")
            + ((c = (int)((x -= b * 86400) * 0.00027777777f)) + a + b > 0 ? c + " h " : "")
            + ((d = (int)((x -= c * 3600) * 0.01666666666f)) + a + b + c > 0 ? d + " m " : "")
            + ((int)(x - d * 60) + " s");
    }
}
