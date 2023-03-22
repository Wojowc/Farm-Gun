using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Vector3 localDirection;
    Vector3 globalDirection;
    float cameraRotationY;
    [SerializeField]
    float speed = 8f;
    [SerializeField]
    float gravityValue = 5f;
    float normalVelovityYValue = 0.12f;
    float velocityY = 0.0f;

    void Awake()
    {
        //get characterComponent
        characterController = GetComponent<CharacterController>();
        //get the Y value of camera rotation in degrees
        cameraRotationY = FindAnyObjectByType<Camera>().transform.rotation.eulerAngles.y;    
    }

    // Update is called once per frame
    void Update()
    {
        velocityY = characterController.isGrounded ? normalVelovityYValue : gravityValue;
        //get input from player
        localDirection = new Vector3 (Input.GetAxisRaw("Horizontal"), -velocityY, Input.GetAxisRaw("Vertical"));
        //multiply it by the rotation of camera
        globalDirection = (Quaternion.Euler(0, cameraRotationY, 0) * localDirection).normalized;
        //move player
        characterController.Move(globalDirection * speed * Time.deltaTime);
    }
}
