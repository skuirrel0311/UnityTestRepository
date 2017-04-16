using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : BaseCharacterController2D
{
    GameObject cameraObj = null;
    public float cameraDistance = 5.0f;

    protected override void Start()
    {
        base.Start();
        cameraObj = Camera.main.gameObject;
    }

    protected override void Update()
    {
        velocity.x = MyInputManager.GetAxis(MyInputManager.Axis.LeftStick).x;

        anim.SetFloat("MoveSpeed", Mathf.Abs(velocity.x));

        cameraObj.transform.position = (transform.position - (Vector3.forward * cameraDistance)) + (Vector3.up);

        if (MyInputManager.GetButtonDown(MyInputManager.Button.A))
        {
            if(currentState == CharacterState.Grounding || currentState == CharacterState.Landing)
                ActionJump();
        }
    }
}
