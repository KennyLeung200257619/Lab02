using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float gravity = 20.0f;

    private  Vector2 m_beginPos;
    private Vector2 m_dragPos;

    private float m_rotateAngle;
    private Vector3 m_moveDirection = Vector3.zero;
    private float m_speed;
    [SerializeField] private float m_speedOffset = 0.001f;

    private CharacterController m_characterCtrller;
    private Animator m_anim;
    

    void Start ()
    {
        m_characterCtrller = GetComponent <CharacterController>();
        m_anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update() 
    {
        // Check any Touches
        if (Input.touchCount > 0) 
        {
            // Get first touch by array : Input.touches
            Touch currentTouch = Input.touches[0];

            // Check the touch began
            if (currentTouch.phase.Equals(TouchPhase.Began)) 
            {
                m_beginPos = currentTouch.position;
            }

            if (currentTouch.phase.Equals(TouchPhase.Moved)) 
            {
                m_dragPos = currentTouch.position;

                m_rotateAngle = Mathf.Atan2((m_dragPos.y - m_beginPos.y), (m_dragPos.x - m_beginPos.x)) * Mathf.Rad2Deg - 90f;

                if (m_rotateAngle <= 180)
                    m_rotateAngle -= 360;

                m_speed = Mathf.Sqrt(Mathf.Pow(m_dragPos.y - m_beginPos.y, 2) + Mathf.Pow(m_dragPos.x - m_beginPos.x, 2)) * m_speedOffset;

                // Rotate the forward vector towards the target direction by one step
                transform.eulerAngles = new Vector3(0f, m_rotateAngle * -1, 0f);
            }

            if (currentTouch.phase.Equals(TouchPhase.Ended)) 
            {
                // Debug.Log("Ended");
                m_speed = 0f;
                m_rotateAngle = 0f;
            }
        }

        // Control the character movement.
        if(m_characterCtrller.isGrounded){
            m_moveDirection = transform.forward * m_speed;
            // m_moveDirection = Camera.main.transform.forward * m_speed;
            // this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 
            //                                             Camera.main.transform.rotation.eulerAngles.y,
            //                                             this.transform.rotation.eulerAngles.z);
        }

        m_characterCtrller.Move(m_moveDirection);
        m_moveDirection.y -= gravity * Time.deltaTime;

        m_anim.SetInteger ("AnimationPar", Mathf.CeilToInt(m_speed));
    }
}
