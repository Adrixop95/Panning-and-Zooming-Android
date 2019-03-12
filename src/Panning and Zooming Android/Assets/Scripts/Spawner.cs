using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public  GameObject[]    prefabs =   new GameObject[2];
    public  Vector3[]       points  =   new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        SpawnSanta( new Vector3( 8, 6, 0 ), new Vector3( -8, -6, 0 ) );
        SpawnTrees( 20, new Vector3( 10, 8, 0 ), new Vector3( -10, -8, 0  ) );
        SpawnRock(9, new Vector3(10, 8, 0), new Vector3(-10, -8, 0));
    }

    private void SpawnSanta( Vector3 tr, Vector3 bl ) {
        GameObject  santa   =   Instantiate( prefabs[0], transform );
        float       x       =   Random.Range(bl.x, tr.x);
        float       y       =   Random.Range(bl.y, tr.y);


        santa.transform.position    =   new Vector3(
            x, y, 1
        );

        GameObject stone    =   Instantiate( prefabs[1], transform );
        stone.transform.position = new Vector3(
            points[1].x + x,
            points[1].y + y,
            0
        );
    }

    private void SpawnTrees( int n, Vector3 tr, Vector3 bl ) {
        for ( int i = 0; i < n; i++ ) {
            GameObject  prefab  =   prefabs[ Random.Range( 2, 4 ) ];
            GameObject  tree    =   Instantiate( prefab, transform );
            float       x       =   Random.Range(bl.x, tr.x);
            float       y       =   Random.Range(bl.y, tr.y);
            tree.transform.position = new Vector3( x, y, -1 );
        }

    }

    private void SpawnRock( int n, Vector3 tr, Vector3 bl ) {
        for ( int i = 0; i < n; i++ ) {
            GameObject  rock    =   Instantiate( prefabs[1], transform );
            float       x       =   Random.Range(bl.x, tr.x);
            float       y       =   Random.Range(bl.y, tr.y);
            rock.transform.position = new Vector3( x, y, 1 );
        }

    }
}
