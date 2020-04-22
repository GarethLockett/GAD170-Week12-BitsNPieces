using UnityEngine;

/*
    Script: InputMove
    Author: Gareth Lockett
    Version: 1.0
    Description:    Simple example of using Input preconfigured Axis (Eg make sure Horizontal is set up in Edit > Project Settings > Input > Axes)
                    Note: 'Horizontal' preconfigured with arrow keys, A D keys, and joystick 1 (Eg left stick on a Xbox controller)
*/

public class InputMove : MonoBehaviour
{
    // Properties
    public float moveSpeed = 3f;        // How fast (in units per second) the object moves.

    // Methods
    void Update()
    {
        // Move this game object left/right using the preconfigured 'Horizontal' axis.
        this.transform.position += this.transform.right * Input.GetAxis( "Horizontal" ) * Time.deltaTime * this.moveSpeed;
    }
}
