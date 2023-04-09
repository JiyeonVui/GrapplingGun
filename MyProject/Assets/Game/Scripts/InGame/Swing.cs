using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope m_grapplingRope;

    [Header("Layer Settings:")]
    [SerializeField] private bool m_grappleToAll = false;
    [SerializeField] private int m_grappleLayerNumber = 0;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform m_gunHolder; // 
    public Transform m_gunPivot;
    public Transform m_firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody2D;

    [Header("Distance:")]
    [SerializeField] private bool m_hasMaxDistance = false;
    [SerializeField] private float m_maxDistance = 20;

    [Header("Rotation:")]
    [SerializeField] private bool m_rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float m_rotationSpeed = 4;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool m_launchToPoint = true;
    [SerializeField] private LaunchType m_launchType = LaunchType.Physics_Launch;
    [SerializeField] private float m_launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool m_autoConfigureDistance = false;
    [SerializeField] private float m_targetDistance = 1;
    [SerializeField] private float m_targetFrequency = 1;

    [HideInInspector] public Vector2 m_grapplePoint;
    [HideInInspector] public Vector2 m_grappleDistanceVector;

    // Start is called before the first frame update
    void Start()
    {
        m_grapplingRope.enabled = false;
        m_springJoint2D.enabled = false;
    }

    // Update is called once per frame
    

    public void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distaceVector = lookPoint - m_gunPivot.position;

        float angle = Mathf.Atan2(distaceVector.y, distaceVector.x) * Mathf.Rad2Deg;
        if(m_rotateOverTime && allowRotationOverTime)
        {
            m_gunPivot.rotation = Quaternion.Lerp(m_gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * m_rotationSpeed);
        }
        else
        {
            m_gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    public void SetGrapplePoint(Vector3 destination)
    {
        if(Vector2.Distance(destination, m_firePoint.position) <= m_maxDistance || !m_hasMaxDistance)
        {
            m_grapplePoint = destination;
            m_grappleDistanceVector = m_grapplePoint - (Vector2)m_gunPivot.position;
            m_grapplingRope.enabled = true;
        }

    }
    public void Pull()
    {
        // rotate
        if (m_grapplingRope.enabled)
        {
            RotateGun(m_grapplePoint, false);
        }
        //
        if(m_launchToPoint && m_grapplingRope.isGrappling)
        {
            if(m_launchType == LaunchType.Transform_Launch)
            {
                Vector2 firePointDistance = m_firePoint.position - m_gunHolder.localPosition;
                Vector2 targetPos = m_grapplePoint - firePointDistance;
                m_gunHolder.position = Vector2.Lerp(m_gunHolder.position, targetPos, Time.deltaTime * m_launchSpeed);
            }
        }
    }
    public void Release()
    {
        m_grapplingRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody2D.gravityScale = 1;
    }


    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if(!m_launchToPoint && !m_autoConfigureDistance)
        {
            m_springJoint2D.distance = m_targetDistance;
            m_springJoint2D.frequency = m_targetFrequency;
        }
        if (!m_launchToPoint)
        {
            if (m_autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = m_grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (m_launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = m_grapplePoint;

                    Vector2 distanceVector = m_firePoint.position - m_gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = m_launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;

                case LaunchType.Transform_Launch:
                    m_rigidbody2D.gravityScale = 0;
                    m_rigidbody2D.velocity = Vector2.zero;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(m_firePoint != null && m_hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(m_firePoint.position, m_maxDistance);
        }
    }

}
