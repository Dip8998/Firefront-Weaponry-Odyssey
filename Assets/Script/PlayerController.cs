using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera myCam;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float gravity;

    public float lookSpeed;
    public float lookXLimit;

    Vector3 moveDir = Vector3.zero;
    float rotX = 0;

    public bool canMove = true;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

  
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float xMove = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float yMove = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float moveDirY = moveDir.y;
        moveDir = (forward * xMove) + (right * yMove);

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDir.y = jumpSpeed;
        }
        else
        {
            moveDir.y = moveDirY;
        }
        if(!characterController.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * Time.deltaTime);

        if(canMove)
        {
            rotX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotX = Mathf.Clamp(rotX ,-lookXLimit,lookXLimit);
            myCam.transform.localRotation = Quaternion.Euler(rotX,0,0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            

        }
    }
}
