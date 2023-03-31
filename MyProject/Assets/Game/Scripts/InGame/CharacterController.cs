using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    public float Time_CounDown = 2f;
    private float Counter;
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2D.enabled = false;
        Counter = this.Time_CounDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint2D.connectedAnchor = mousePos;
            distanceJoint2D.enabled = true;
            lineRenderer.enabled = true;
            AddForce();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }
        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            AddForce();
        }

    }

    void AddForce()
    {
        if(Counter > 0)
        {
          
            Counter -= Time.deltaTime;
            if(Counter <= 0)
            {
                Debug.Log("Add Force");
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f,100f)*gameObject.GetComponent<Rigidbody2D>().velocity.normalized);
                Counter = this.Time_CounDown;
            }
        }
    }
}
