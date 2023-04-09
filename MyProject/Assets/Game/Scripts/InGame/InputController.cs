using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public enum swingState
{
    shoot,
    hold,
    release
}


public class InputController : MonoBehaviour
{
    public static Action leftAction;
    public static Action rightAction;
    public static Action jumpAction;
    public static Action releaseMove;
    public static Action<swingState> swing;

    [SerializeField] private Button m_buttonLeft;
    [SerializeField] private Button m_buttonRight;
    [SerializeField] private Button m_buttonJump;

    private bool m_stop;
    private bool m_leftEnter;
    private bool m_rightEnter;
    private bool m_jumpEnter;
    private swingState m_swingState = swingState.release;

    private void Awake()
    {
        GameController.readyPlayEvent += OnReady;
        GameController.resumePlayerEvent += Resume;
        GameController.stopPlayerEvent += Stop;
    }
    private void OnDestroy()
    {
        GameController.readyPlayEvent -= OnReady;
        GameController.resumePlayerEvent -= Resume;
        GameController.stopPlayerEvent -= Stop;
    }
    private void OnReady()
    {
        m_stop = false;
    }

    public void OnLeftPointerDown(BaseEventData eventData)
    {

    }

    public void OnRightPointerDown(BaseEventData eventData)
    {

    }

    public void OnJumpPointerDown(BaseEventData eventData)
    {

    }

    public void OnSwingPointerDown(BaseEventData eventData)
    {
        m_swingState = swingState.shoot;
    }


    public void OnLeftPointerUp(BaseEventData eventData)
    {

    }

    public void OnRightPointerUp(BaseEventData eventData)
    {

    }

    public void OnJumpPointerUp(BaseEventData eventData)
    {

    }
    public void OnSwingPointerUp(BaseEventData eventData)
    {
        m_swingState = swingState.release;
    }
    public void OnLeftPointerEnter(BaseEventData eventData)
    {
        m_leftEnter = true;
    }
    public void OnRightPointerEnter(BaseEventData eventData)
    {
        m_rightEnter = true;
    }
    public void OnJumpPointerEnter(BaseEventData eventData)
    {
        m_jumpEnter = true;
    }
    public void OnSwingPointerEnter(BaseEventData eventData)
    {
        m_swingState = swingState.hold;
    }

    public void OnLeftPointerExit(BaseEventData eventData)
    {
        m_jumpEnter = false;
    }

    public void OnRightPointerExit(BaseEventData eventData)
    {
        m_rightEnter = false;
    }

    public void OnJumpPointerExit(BaseEventData eventData)
    {
        m_jumpEnter = false;
    }
    public void OnSwingPointerExit(BaseEventData eventData)
    {
        m_swingState = swingState.release;
    }
    private void Update()
    {
        if (m_stop)
        {
            return;
        }

        if((Input.touchCount > 0 && m_leftEnter) || Input.GetKey(KeyCode.LeftArrow))
        {
            leftAction?.Invoke();
        }
        if((Input.touchCount > 0 && m_rightEnter) || Input.GetKey(KeyCode.RightArrow))
        {
            rightAction?.Invoke();
        }

        if((Input.touchCount > 0 && m_jumpEnter) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            jumpAction?.Invoke();
            releaseMove?.Invoke();
            swing?.Invoke(m_swingState);
        }
        if(Input.touchCount == 0)
        {
            swing?.Invoke(m_swingState);
        }



        if(!m_rightEnter && !m_leftEnter && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            releaseMove?.Invoke();
        }
    }

    public void Stop()
    {
        m_stop = true;
    }

    public void Resume()
    {
        m_stop = false;
    }
}
