using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject FollowTarget;
    Transform FollowTargetTransform;
    Rigidbody FollowTargetRigidbody;
    float f_MinimumCameraDistance = -10f;
    float f_MaximumCameraDistance = -16f;
    float f_CameraMoveSpeedModifier =  0.5f;

    private Vector3 myVelo = Vector3.zero;

    Vector3 v3_CurrentTargetPosition;

	// Use this for initialization
	void Start () {
        FollowTarget = GameObject.FindGameObjectWithTag("Player");
        FollowTargetTransform = FollowTarget.transform;
        FollowTargetRigidbody = FollowTarget.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        v3_CurrentTargetPosition = FollowTargetTransform.position;
        //v3_CurrentTargetPosition.z = gameObject.transform.position.z;

        Vector3 v3_TempVec = v3_CurrentTargetPosition - gameObject.transform.position;

        if (FollowTargetRigidbody.velocity.x != 0f || FollowTargetRigidbody.velocity.y != 0f )
        {
            //Debug.Log(Vector3.Magnitude(v3_TempVec));
            float tempZ = f_MinimumCameraDistance - (Vector3.Magnitude(FollowTargetRigidbody.velocity) * f_CameraMoveSpeedModifier);
            v3_CurrentTargetPosition.z = tempZ;
            
        }
        
        if( v3_CurrentTargetPosition.z > f_MinimumCameraDistance )
        {
            v3_CurrentTargetPosition.z = f_MinimumCameraDistance;
        }
        else if (v3_CurrentTargetPosition.z < f_MaximumCameraDistance)
        {
            v3_CurrentTargetPosition.z = f_MaximumCameraDistance;
        }


        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, v3_CurrentTargetPosition, ref myVelo, Time.deltaTime * 0.2f);
        FollowTarget.SendMessage("NewZ", gameObject.transform.position.z);
    }
}
