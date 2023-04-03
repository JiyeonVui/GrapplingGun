using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Vector3 m_position;
    private float m_width;
    private float m_height;
    private Rigidbody2D m_rigidbody2D;
    [SerializeField] Transform m_pivotList;
    private Transform m_pivotCurrent;
    private void Awake()
    {
        m_width = (float)Screen.width / 2f;
        m_height = (float)Screen.height / 2f;

        m_position = new Vector3(0.0f, 0.0f, 0.0f);
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
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
        UpdatePivot();

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                // slow motion
                SlowMotion(true);
                GetComponent<GrapplingGun>().SetGrapplePoint();
            }

            if(touch.phase == TouchPhase.Ended)
            {
                //
                SlowMotion(false);
                GetComponent<GrapplingGun>().Release();
            }
        }
    }


}
