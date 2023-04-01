using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Vector3 m_position;
    private float m_width;
    private float m_height;

    private void Awake()
    {
        m_width = (float)Screen.width / 2f;
        m_height = (float)Screen.height / 2f;

        m_position = new Vector3(0.0f, 0.0f, 0.0f);
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

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                // slow motion
                SlowMotion(true);
            }

            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // change direction
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                GetComponent<GrapplingGun>().RotateGun(touchPos, false);

            }

            if(touch.phase == TouchPhase.Ended)
            {
                //
                SlowMotion(false);
                GetComponent<GrapplingGun>().SetGrapplePoint();
            }
        }
        else
        {
            GetComponent<GrapplingGun>().Release();
        }
    }
}
