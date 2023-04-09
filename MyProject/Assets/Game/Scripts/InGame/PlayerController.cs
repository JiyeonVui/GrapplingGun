using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    private float m_width;
    private float m_height;
    private float m_Velocity = 20f;
    private bool m_isFacingRight = true;
    private Rigidbody2D m_rigidbody2D;
    [SerializeField] Transform m_pivotList;
    [SerializeField] private Transform m_pivotCurrent;
    [SerializeField] private Swing m_swing;
    private State m_state;
    private Vector3 m_position;

    
    public bool isGrounded
    {
        get
        {
            return m_state == State.Ground;
        }
    }
    private enum State
    {
        Ground,
        Air,
        Free
    }

    private void Awake()
    {
        InputController.leftAction += OnMoveLeft;
        InputController.rightAction += OnMoveRight;
        InputController.jumpAction += OnJump;
        InputController.releaseMove += OnReleaseMove;
        InputController.swing += Swing;

        m_width = (float)Screen.width / 2f;
        m_height = (float)Screen.height / 2f;

        m_position = new Vector3(0.0f, 0.0f, 0.0f);
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
    }
    private void OnDestroy()
    {
        InputController.leftAction -= OnMoveLeft;
        InputController.rightAction -= OnMoveRight;
        InputController.jumpAction -= OnJump;
        InputController.releaseMove -= OnReleaseMove;
        InputController.swing -= Swing;
    }

    private void Start()
    {

    }

    private void UpdatePivot()
    {
        Vector2 temp = transform.position;
        float distance = float.MaxValue;
        foreach(Transform pos in m_pivotList)
        {
            float tempDistance = Vector2.Distance(pos.position, temp);
            if(tempDistance < distance)
            {
                distance = tempDistance;
                m_pivotCurrent = pos;
            }
        }
        // hight light

        
    }
    private void Shooting()
    {
        GetComponent<GrapplingGun>().SetGrapplePointNew(m_pivotCurrent.position);
    }
    private void OnMoveLeft()
    {
        Move(-1);
    }
    private void OnMoveRight()
    {
        Move(1);
    }

    private void OnJump()
    {
        Jump();
    }
    private void OnReleaseMove()
    {
        if(m_state == State.Ground)
        {
            m_rigidbody2D.velocity = Vector3.zero;
        }
    }

    private void Move(int direction)
    {
        if (m_isFacingRight)
        {
            if(direction < 0)
            {
                Flip();
            }

        }
        else
        {
            if(direction > 0)
            {
                Flip();
            }
        }


        if (isGrounded)
        {
            // ground
            m_rigidbody2D.velocity = new Vector2(direction * m_Velocity, m_rigidbody2D.velocity.y);
        }
        else if (m_state == State.Air)
        {
            // air  
        }
        else
        {

        }

        
    }
    private void Flip()
    {
        m_isFacingRight = !m_isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    private void Jump()
    {

    }
    private void Swing(swingState m_swingState)
    {
        //Debug.Log(m_swingState);
        if(m_swingState == swingState.shoot)
        {
            // set grapple point
            m_swing.GetComponent<Swing>().SetGrapplePoint(m_pivotCurrent.position);
            //Debug.Log(m_pivotCurrent.position);
        }
        if(m_swingState == swingState.hold)
        {
            // hold
            m_swing.GetComponent<Swing>().Pull();
        }
        if(m_swingState == swingState.release)
        {
            // release
            m_swing.GetComponent<Swing>().Release();
        }
    }
    

    private void SlowMotion(bool isSlow)
    {
        if (isSlow)
        {
            // slow
        }
        else
        {

        }
    }




}
