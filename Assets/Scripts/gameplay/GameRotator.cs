using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GameRotator : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float sensitivity;
    [SerializeField] float lerpSmooth;

    private float currentAngle;
    private float targetAngle;

    /*private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }*/

    private void FixedUpdate()
    {
        float target = Mathf.Lerp(currentAngle, targetAngle, lerpSmooth * Time.deltaTime);
        // float target = targetAngle;
        float delta = target - currentAngle;
        float max = maxSpeed * Time.deltaTime; 
        delta = Mathf.Clamp(delta, -max, max);
        currentAngle += delta;

        // body.MoveRotation(body.rotation * Quaternion.Euler(Vector3.forward * delta));
        transform.Rotate(Vector3.forward * delta);
    }

    public void Rotate(float deltaX)
    {
        targetAngle += deltaX * sensitivity;
    }
}
