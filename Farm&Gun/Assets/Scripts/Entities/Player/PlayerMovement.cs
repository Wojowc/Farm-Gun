using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    bool canMove = true;
    Vector3 inputDirection;
    Vector3 correctedInputDirection;
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
        cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        //movement guard
        if (!canMove) return;

        HandleRotation();

        //determine if falling
        velocityY = characterController.isGrounded ? normalVelovityYValue : gravityValue;

        //get input from player
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), -velocityY, Input.GetAxisRaw("Vertical"));

        //multiply it by the rotation of camera
        correctedInputDirection = (Quaternion.Euler(0, cameraRotationY, 0) * inputDirection).normalized;

        //move player
        characterController.Move(correctedInputDirection * speed * Time.deltaTime);
    }

    void HandleRotation()
    {
        //get mouse and player position 2d
        Vector3 playerPos2d = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos2d = Input.mousePosition;

        //cast player and mouse position to direction
        Vector3 mouseDirection3d = new Vector3(mousePos2d.x - playerPos2d.x, transform.position.y, mousePos2d.y - playerPos2d.y);
        Vector3 correctedMouseDirection3d = Quaternion.Euler(0, cameraRotationY, 0) * mouseDirection3d;

        //rotate player
        transform.LookAt(transform.position + correctedMouseDirection3d);
    }

    public void DisableMovement(float time)
    {
        Invoke("EnableMovement", time);
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public bool IsEnabled()
    {
        return canMove;
    }
}
