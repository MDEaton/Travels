using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    float f_Impulse = 15f;
    float f_StopSpeed = 3f;
    Rigidbody myRigidBody;

    bool FullStop = false;

    float CameraZ;

    Vector3 target;
    Vector3 mouse;
    Vector3 shipPropulsion;

    public GameObject LeftBound, RightBound, UpperBound, LowerBound;

    private bool PlayerControlsEnabled = true;

    // Use this for initialization
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && PlayerControlsEnabled)
        {
            RaycastHit rayHit;
            Ray rayman = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayman, out rayHit))
            {
                if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("player"))
                {
                    Debug.Log("STOP");
                    FullStop = true;
                }
                else
                {
                    DoMove();
                }
            }
            else
            {
                /*if( myRigidBody.velocity.x > 6f )
                {
                    myRigidBody.velocity = new Vector3(6f, myRigidBody.velocity.y, myRigidBody.velocity.z);
                }
                else if(myRigidBody.velocity.x < -6f)
                {
                    myRigidBody.velocity = new Vector3(-6f, myRigidBody.velocity.y, myRigidBody.velocity.z);
                }

                if (myRigidBody.velocity.y > 3f)
                {
                    myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, 3f, myRigidBody.velocity.z);
                }
                else if (myRigidBody.velocity.y < -3f)
                {
                    myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -3f, myRigidBody.velocity.z);
                }*/
                //myRigidBody.velocity = Vector3.zero;
                DoMove();


                //myRigidBody.AddForce(shipPropulsion, ForceMode.Impulse);
            }
        }
        if (FullStop)
        {
            myRigidBody.velocity = Vector3.Lerp(myRigidBody.velocity, Vector3.zero, Time.deltaTime * f_StopSpeed);
        }
    }

    void NewZ(float z)
    {
        CameraZ = z;
    }

    void Die ()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        StartCoroutine(PlayerDied());
    }

    IEnumerator PlayerDied ()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    void DoMove()
    {
        FullStop = false;
        mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(CameraZ);
        target = Camera.main.ScreenToWorldPoint(mouse);

        gameObject.transform.LookAt(target);
        if (target.x < LeftBound.transform.position.x)
        {
            target.x = LeftBound.transform.position.x;
        }
        else if (target.x > RightBound.transform.position.x)
        {
            target.x = RightBound.transform.position.x;
        }

        if (target.y < LowerBound.transform.position.y)
        {
            target.y = LowerBound.transform.position.y;
        }
        else if (target.y > UpperBound.transform.position.y)
        {
            target.y = UpperBound.transform.position.y;
        }

        shipPropulsion = target - gameObject.transform.position;

        


        myRigidBody.AddForce(shipPropulsion.normalized * f_Impulse, ForceMode.Acceleration);
    }

}
