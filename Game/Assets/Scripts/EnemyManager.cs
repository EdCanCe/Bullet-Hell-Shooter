using UnityEngine;
using System;

class EnemyManager : MonoBehaviour
{
    private static float baseSpeed = 100;
    private static float baseAngle = -90;

    public static Vector3 RotateVector(Vector3 vector, float degrees) {
        // Gets the vector's magnitude
        float size = vector.magnitude;

        // In case the y-axis is negative it turns it negative
        float currentAngle = Vector3.Angle(vector, Vector3.right) * (vector.y < 0 ? -1 : 1);
        
        float finalAngle = currentAngle + degrees;

        // The degrees are turned to radians
        float radians = finalAngle * (float)Math.PI / 180f;

        // The values of the new vector
        float xValue = (float)Math.Cos(radians);
        float yValue = (float)Math.Sin(radians);

        // The vector is made
        return new Vector3(xValue, yValue, 0) * size;
    }

    private static Vector3 CreateVector(float magnitude, float degrees) {
        // Rotates a vector always pointing to the right
        return RotateVector(Vector3.right, degrees) * magnitude;
    }

    public static void Place(GameObject parent, GameObject enemy, Vector3 position, float rotation, float extraSpeed, float acceleration) {
        GameObject enemyObject = Instantiate(enemy, position, Quaternion.Euler(0, 0, rotation), parent.transform);
        
        // I looked in chatGPT how to access the EnemyMovement component from another class
        EnemyMovement movement = enemyObject.GetComponent<EnemyMovement>();

        Vector3 angleSpeed = CreateVector(baseSpeed + extraSpeed, rotation);

        movement.speed = angleSpeed;
        movement.acceleration = acceleration;
    }

    public static void PlaceRound(GameObject parent, GameObject enemy, Vector3 position, int amount, float extraAngle, float extraSpeed, float acceleration) {
        // I get the degrees each bullet will be away from each other
        float separation = 360f / amount;

        for (int i = 0; i < amount; i++) {
            Place(parent, enemy, position, baseAngle + extraAngle + separation * i, extraSpeed, acceleration);
        }
    }

}