using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public  GameObject[]    prefabs =   new GameObject[2];
    public  Vector3[]       shifts  =   new Vector3[2];

    // ##########################################################################################
    void Start() {
        SpawnSanta( new Vector3( 8, 6, 0 ), new Vector3( -8, -6, 0 ) );
        SpawnTrees( 20, new Vector3( 10, 8, 0 ), new Vector3( -10, -8, 0  ) );
        SpawnRock(9, new Vector3( 10, 8, 0 ), new Vector3( -10, -8, 0) );
    }

    // ##########################################################################################
    private void SpawnSanta( Vector3 tr, Vector3 bl ) {
        GameObject  santa   =   Instantiate( prefabs[0], transform );
        GameObject  stone   =   Instantiate( prefabs[1], transform );

        float       x       =   Random.Range(bl.x, tr.x);
        float       y       =   Random.Range(bl.y, tr.y);

        santa.transform.position    =   new Vector3( x + shifts[0].x, y + shifts[0].y, 2 );
        stone.transform.position    =   new Vector3( x + shifts[1].x, y + shifts[1].y, 1 );
    }

    // ------------------------------------------------------------------------------------------
    private void SpawnTrees( int n, Vector3 tr, Vector3 bl ) {
        for ( int i = 0; i < n; i++ ) {
            int         range   =   Random.Range( 2, 4 );
            GameObject  prefab  =   prefabs[ range ];
            GameObject  tree    =   Instantiate( prefab, transform );
            float       x       =   Random.Range(bl.x, tr.x);
            float       y       =   Random.Range(bl.y, tr.y);
            tree.transform.position = new Vector3( x, y, 0 );
        }
    }

    // ------------------------------------------------------------------------------------------
    private void SpawnRock( int n, Vector3 tr, Vector3 bl ) {
        for ( int i = 0; i < n; i++ ) {
            GameObject  rock    =   Instantiate( prefabs[1], transform );
            float       x       =   Random.Range(bl.x, tr.x);
            float       y       =   Random.Range(bl.y, tr.y);
            rock.transform.position = new Vector3( x, y, 1 );
        }
    }

    // ##########################################################################################
}
