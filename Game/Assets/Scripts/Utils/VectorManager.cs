using UnityEngine;
using System;

/// <summary>
/// Methods to create and rotate Vectors.
/// </summary>
static class VectorManager
{
    /// <summary>
    /// Rotates a vector depending on the degrees given.
    /// </summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="degrees">The angle in degrees to rotate the vector.</param>
    /// <returns>The rotated vector</returns>
    public static Vector3 Rotate(Vector3 vector, float degrees)
    {
        // Gets the vector's magnitude
        float size = vector.magnitude;

        // In case the y-axis is negative it turns it negative
        float currentAngle = Vector3.Angle(vector, Vector3.right) * (vector.y < 0 ? -1 : 1);
        
        float finalAngle = currentAngle + degrees;

        // With the help of ChatGPT I figured out that I needed to use radians
        float radians = finalAngle * (float)Math.PI / 180f;

        // The values of the new vector
        float xValue = (float)Math.Cos(radians);
        float yValue = (float)Math.Sin(radians);

        // The vector is made
        return new Vector3(xValue, yValue, 0) * size;
    }

    /// <summary>
    /// Creates a vector depending on the magnitude and degrees given.
    /// </summary>
    /// <param name="magnitude">The magnitude of the vector.</param>
    /// <param name="degrees">The angle the vector will point to.</param>
    /// <returns>The new vector</returns>
    public static Vector3 Create(float magnitude, float degrees)
    {
        // Rotates a vector always pointing to the right
        return Rotate(Vector3.right, degrees) * magnitude;
    }
}