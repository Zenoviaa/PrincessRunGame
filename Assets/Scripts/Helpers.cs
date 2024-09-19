using System;
using UnityEngine;


public static class Helpers
{
    private static UnityEngine.Camera _mainCamera;
    public const float PPU = 16;
    public static Vector3 PixelSnap(Vector3 targetPosition)
    {
        Vector3 pixelPosition = targetPosition;
        pixelPosition.x = (Mathf.Round(pixelPosition.x * PPU) / PPU);
        pixelPosition.y = (Mathf.Round(pixelPosition.y * PPU) / PPU);
        pixelPosition.z = (Mathf.Round(pixelPosition.z * PPU) / PPU);
        return pixelPosition;
    }

    public static Vector3 PixelSnap(Transform parent, SpriteRenderer spriteRenderer)
    {
        float x = (Mathf.Round(parent.position.x * PPU) / PPU) - parent.position.x;
        float y = (Mathf.Round(parent.position.y * PPU) / PPU) - parent.position.y;
        Vector3 finalPosition = new Vector3(x * spriteRenderer.transform.lossyScale.x, y * spriteRenderer.transform.lossyScale.y, 0);
        return finalPosition;
    }

    /// <summary>
    /// Returns the main camera, which is the one following Skye.
    /// </summary>
    /// <returns></returns>
    public static UnityEngine.Camera GetMainCamera()
    {
        if (_mainCamera == null)
            _mainCamera = UnityEngine.Camera.main;
        return _mainCamera;
    }

    /// <summary>
    /// Returns the world position of the camera, this will completely disregard the z-axis.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMainCameraWorldPosition()
    {
        Vector3 position = GetMainCamera().transform.position;
        position.z = 0.0f;
        return position;
    }

    /// <summary>
    /// Returns the world position of the mouse.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 position = GetMainCamera().ScreenToWorldPoint(Input.mousePosition);
        position.z = 0.0f;
        return position;
    }

    public static Quaternion GetAngle(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Quaternion GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Color ChangeAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static float Osc(float from, float to, float speed = 1f, float offset = 0f)
    {
        float dif = (to - from) / 2f;
        return from + dif + dif * (float)Math.Sin(Time.time * speed + offset);
    }
}

