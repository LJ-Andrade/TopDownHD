using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    // public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool Walk;
    public bool Sprint;
    public bool Crouch;
    public bool MouseWheelUp;
    public bool MouseWheelDown;
    public bool Key1;
    public bool Key2;
    public bool Jump;


    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        // Jump = Input.GetButtonDown("Jump");
        // Walk = Input.GetButton("Walk");
        // MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetMouseButton(0);
        Reload = Input.GetKey(KeyCode.R);
        // Sprint = Input.GetButton("Sprint");
        // Crouch = Input.GetButton("Crouch");
        // MouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        // MouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        Key1 = Input.GetKey(KeyCode.Alpha1);
        Key2 = Input.GetKey(KeyCode.Alpha2);
    }
}
