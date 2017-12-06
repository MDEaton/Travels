using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour {

    public GameObject Mines;
    List<GameObject> MinePool = new List<GameObject>();

    float NextMineTimer = 0f;
    float TimeBetweenMines = 2f;

	// Use this for initialization
	void Start ()
    {
	    for( int x = 0; x < 50; x++)
        {
            GameObject newMine = Instantiate(Mines, transform.position, Quaternion.identity);
            newMine.SetActive(false);
            MinePool.Add(newMine);
        }

	}
	
	// Update is called once per frame
	void Update () {

        if ( NextMineTimer <= 0f)
        {
            DropMine();
            NextMineTimer = TimeBetweenMines;
        }

        NextMineTimer -= Time.deltaTime;

	}

    void DropMine ()
    {
        for( int x = 0; x < MinePool.Count; x++)
        {
            if(!MinePool[x].gameObject.activeInHierarchy)
            {
                Vector3 StartPosition = gameObject.transform.position;
                StartPosition.x = Random.Range(-90f, 90f);
                MinePool[x].transform.position = StartPosition;
                MinePool[x].SetActive(true);
                break;
            }
        }
    }
}
