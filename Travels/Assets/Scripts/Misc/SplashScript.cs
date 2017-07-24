using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour {

    public GameObject ParticleSplash;

    GameObject Splash;

    float splashTimer = 1f;
    float splashCounter;
    bool deleteHim = false;
	// Use this for initialization
	void Update ()
    {

	}

    void OnTriggerEnter(Collider c)
    {
        
        if( c.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("SPLASH");
            if ( ParticleSplash != null )
            {
                Splash = (GameObject)Instantiate(ParticleSplash, new Vector3(c.transform.position.x, gameObject.transform.position.y, c.transform.position.z), Quaternion.identity);
                //splashCounter = 0f;
                //deleteHim = true;
                Destroy(Splash, 0.5f);
            }
        }
    }

}
