using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMine : MonoBehaviour {

    float RotationSpeed = -0.1f;

    float CountdownToExplode = 0f;
    public float BombFuse = 5f;

    //Renderer myRenderer;

    float PulseSpeed = 3f;
    float pulseCounter = 0f;

    bool countdown = false;
    Color CurrentColour = Color.white;

    public Renderer[] ChildrenRenderer;

    public GameObject Explosion;
    float ThreatDistance = 5f;

    bool Sinking = true;

    //float fSinkSpeed = 0.01f;
    Rigidbody myRigidbody;

	// Use this for initialization
	void Start ()
    {
        TurnGreen();
        for( int x = 0; x < ChildrenRenderer.Length; x++)
        {
            //myRenderer = gameObject.GetComponent<Renderer>();
        }
        myRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        
        for (int x = 0; x < ChildrenRenderer.Length; x++)
        {
            ChildrenRenderer[x].material.color = CurrentColour;
        }

        float emissionfloat = Mathf.PingPong(Time.fixedTime, 1f);
        Color tempEmissionColor = CurrentColour * emissionfloat;


        pulseCounter += Time.deltaTime;
        if(pulseCounter >= PulseSpeed)
        {
            pulseCounter = 0f;
            
            for (int x = 0; x < ChildrenRenderer.Length; x++)
            {
                ChildrenRenderer[x].material.SetColor("_EmissionColor", tempEmissionColor);
            }
        }

        if( countdown )
        {
            CountdownToExplode += Time.deltaTime;
        }

        if( CountdownToExplode >= BombFuse)
        {
            //explode
            ExplodeMe();
        }

        if( Sinking )
        {

            //Vector3 tempFallRate = gameObject.transform.position;
            //tempFallRate -= Vector3.up * fSinkSpeed;
            //gameObject.transform.position = tempFallRate;
            gameObject.transform.Rotate(Vector3.one * RotationSpeed);
        }
	}

    void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            //transition to yellow
            Debug.LogWarning("BEEP");
            TurnYellow();
        }
    }

    void OnTriggerStay (Collider c)
    {
        
        if( c.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            float distance = Vector3.Distance(gameObject.transform.position, c.gameObject.transform.position);
            if( distance < ThreatDistance )
            {
                TurnRed();
            }
            else
            {
                TurnYellow();
            }
        }
    }

    void OnTriggerExit (Collider c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("player"))
        {
                TurnGreen();
        }
    }

    void TurnGreen ()
    {
        CurrentColour = Color.green;// Color.Lerp(CurrentColour, Color.green, 1f);
        countdown = false;
        CountdownToExplode = 0;
    }

    void TurnYellow ()
    {
        CurrentColour = Color.yellow;// Color.Lerp(CurrentColour, Color.yellow, 1f);
        countdown = false;
    }

    void TurnRed ()
    {
        CurrentColour = Color.red;// Color.Lerp(CurrentColour, Color.red, 1f);
        countdown = true;
    }

    void ExplodeMe ()
    {
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        //Destroy(gameObject);
        gameObject.SetActive(false);
        //gameObject.transform.position = gameObject.transform.parent.transform.position;
    }

    void OnCollisionEnter ( Collision c)
    {
        Debug.Log("collided");
        if( c.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            Sinking = false;
            //myRigidbody.useGravity = false;
            //myRigidbody.isKinematic = true;
        }
        else if(c.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            ExplodeMe();
        }
    }
}
