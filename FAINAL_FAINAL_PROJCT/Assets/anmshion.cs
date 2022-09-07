using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anmshion : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public Transform cam;
    float Gravity = 10;
    float verticalVelocity = 0;
    public float jumpValue = 10;
    Animator anim;
   
    

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
        



    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isSprint = Input.GetKey(KeyCode.LeftShift);

        float Sprint = isSprint ? 1.8f : 1f;

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        if (Controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpValue;
            }
        }
        else
        {
            verticalVelocity -= Gravity * Time.deltaTime;
        }

        anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint ? 0.5f : 0));

        if (moveDirection.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        moveDirection = cam.TransformDirection(moveDirection);

        moveDirection = new Vector3(moveDirection.x * Speed * Sprint, verticalVelocity, moveDirection.z * Speed * Sprint);
        Controller.Move(moveDirection * Time.deltaTime);
    }

}

