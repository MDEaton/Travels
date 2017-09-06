using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour {

    GameObject Mines;
    List<GameObject> MinePool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnMine ()
    {
        for( int x = 0; x < MinePool.Count; x++)
        {
            if(!MinePool[x].gameObject.activeInHierarchy)
            {
                //instantiate MINEPOOL[x]
            }
        }
    }
}
