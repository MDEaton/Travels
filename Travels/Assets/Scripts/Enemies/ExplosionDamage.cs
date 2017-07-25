using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour {

    float Lifetime = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Lifetime -= Time.deltaTime;

        if( Lifetime <= 0f)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter (Collision c)
    {
        if( c.gameObject.layer == LayerMask.NameToLayer("player") )
        {
            //do damage
        }
    }
}
