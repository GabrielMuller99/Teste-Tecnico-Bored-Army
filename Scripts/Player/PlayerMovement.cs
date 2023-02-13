using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRotationSpeed;

    void Start()
    {
    }

    void Update()
    {
        float playerMovementY = Input.GetAxis("Vertical");
        float playerRotation = Input.GetAxis("Horizontal");

        if (playerMovementY <= 0)
        {
            playerMovementY = 0;
        }

        transform.Translate(transform.up * playerMovementY * playerSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * playerRotation * playerRotationSpeed * -1f * Time.deltaTime);
    }
}
