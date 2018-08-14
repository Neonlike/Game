using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public Joystick joystick;
    Vector3 position;

    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }
    void Update()
    {
        position = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);
        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            this.transform.up = Vector3.Lerp(this.transform.up, this.position.normalized, 3.5f);
    }


}
