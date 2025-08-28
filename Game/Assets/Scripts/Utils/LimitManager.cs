using UnityEngine;

/// <summary>
/// Based off the game area, gets its coords limits.
/// </summary>
static class LimitManager
{
    /// <summary>
    /// Gets the limits with an added extra padding.
    /// </summary>
    /// <param name="area">The game area object.</param>
    /// <param name="extra">The extra padding to add.</param>
    /// <returns></returns>
    static public float[] GetLimits(GameObject area, float extra)
    {
        // Gets the coordinates of the game area corners
        Vector3[] corners = new Vector3[4];
        area.GetComponent<RectTransform>().GetWorldCorners(corners);

        // Gets the axis limits of the game area
        float minX = corners[0].x + extra;
        float minY = corners[0].y + extra;
        float maxX = corners[2].x - extra;
        float maxY = corners[2].y - extra;

        return new float[] { minX, minY, maxX, maxY };
    }
}