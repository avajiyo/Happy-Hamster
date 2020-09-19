using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPilot : MonoBehaviour
{

    public bool autoPilotOn = false;
    public bool simpleAutoPilot = false;
    public Transform target;
    Vector3 dirToTarget;

    const float speed = 3.0f;
    public float rotationSpeed = 100.0f;

    void Move()
    {
        RotateTowards();

        //Vector3.Distance() can simplify this process:
        float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(target.transform.position.z - this.transform.position.z, 2));

        if (distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void RotateTowards()
    {
        Vector3 myDirection = transform.forward;
        dirToTarget = target.transform.position - this.transform.position;
        float dotProduct = Vector3.Dot(myDirection, dirToTarget);
        //Alternatively, dotProduct = myDirection.x * dirToTarget.x + myDirection.z * dirToTarget.z;

        float angle = Mathf.Acos(dotProduct/(myDirection.magnitude * dirToTarget.magnitude));
        float crossZ = Vector3.Cross(myDirection, dirToTarget).z;


        if (Mathf.Abs(angle) > 1)
        {

            int rotationDir = 1;
            if (crossZ < 0)
            {
                rotationDir = -1;
            }

            //Note: Rotate it on the y-axis in 3D space. Rotate on the z-axis if you're 2D space / top-down view
            transform.Rotate(0, angle * rotationDir * Mathf.Rad2Deg, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!autoPilotOn)
            {
                autoPilotOn = true;
            } else
            {
                autoPilotOn = false;
            }
        }

        if (autoPilotOn)
        {
            Move();
        }
       

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!simpleAutoPilot)
            {
                simpleAutoPilot = true;
            }
            else
            {
                simpleAutoPilot = false;
            }
           
        }
        if (simpleAutoPilot)
        {
            MoveSimpler();
        }

        void MoveSimpler()
        {
            transform.LookAt(target);

            //Vector3.Distance() can simplify this process:
            float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(target.transform.position.z - this.transform.position.z, 2));

            if (distance > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

}
