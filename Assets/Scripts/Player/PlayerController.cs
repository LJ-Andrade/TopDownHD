using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof (WeaponController))]
[RequireComponent (typeof (Player))]
public class PlayerController : MonoBehaviour
{
    public bool useController;
    public bool freeMovement = false;
    
    WeaponController weaponController;
    Player player;
    private Vector3 moveVelocity;

    Vector3 velocity;
    Vector3 lookPos;
    Rigidbody rb;
    float horizontal;
    float vertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weaponController = GetComponent<WeaponController>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        GetInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        if(freeMovement)
        {
            transform.Translate(moveInput * player.moveSpeed * Time.fixedDeltaTime, Space.World); 
        }
        else
        {
            transform.Translate(moveInput * player.moveSpeed * Time.fixedDeltaTime, Space.Self);
        }
    }

    void GetInput()
    {
        horizontal = GameManager.Instance.InputController.Horizontal;
        vertical = GameManager.Instance.InputController.Vertical;
        if(GameManager.Instance.InputController.Fire1)
            weaponController.Shoot();
    }   

    void Look()
    {
        if(!useController)
        {
            // Use Mouse
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLenght;

            if(groundPlane.Raycast(cameraRay, out rayLenght))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                Debug.DrawLine(transform.position, pointToLook, Color.red);
            }
        }
        else
        {
            // Use Controller
            Vector3 playerDirection = Vector3.right * horizontal + Vector3.forward * vertical;
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }

    }

}
