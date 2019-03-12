using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools {

    public static Vector3 ClampMove( Vector3 current, Vector3 direction, Vector3[] clamps) {
        Vector3 result  =   current + direction;
        return ClampVector( result, clamps );
    }

    public static Vector3 ClampVector( Vector3 vector, Vector3[] clamps ) {
        float   x   =   vector.x > clamps[0].x ? clamps[0].x : vector.x <= clamps[1].x ? clamps[1].x : vector.x;
        float   y   =   vector.y > clamps[0].y ? clamps[0].y : vector.y <= clamps[1].y ? clamps[1].y : vector.y;
        float   z   =   vector.z > clamps[0].z ? clamps[0].z : vector.z <= clamps[1].z ? clamps[1].z : vector.z;
        return  new Vector3( x, y, z );
    }

}
