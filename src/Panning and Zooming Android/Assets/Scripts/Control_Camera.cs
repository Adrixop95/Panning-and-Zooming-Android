using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control_Camera : MonoBehaviour {

    private Vector3     touchStart;
    private int         points      =   0;
    public  float       zoomOutMin  =   1;
    public  float       zoomSpeed   =   8f;
    public  float       zoomOutMax  =   10;
    public  Vector3[]   moveBorders =   new Vector3[2];

    // ##########################################################################################
    void Update() {
        BackButton();
        if (PinchToZoom()) { return; };
        MoveCamera();
        OnTouchObject();
    }

    // ------------------------------------------------------------------------------------------
    private void BackButton() {
        if ( Input.GetKeyUp( KeyCode.Escape ) ) {
            if (Application.platform == RuntimePlatform.Android) {
                string              activity_class  =   "com.unity3d.player.UnityPlayer";
                string              activity_name   =   "currentActivity";
                AndroidJavaObject   activity        =   new AndroidJavaClass( activity_class ).GetStatic<AndroidJavaObject>( activity_name );
                activity.Call<bool>( "moveTaskToBack", true );

            } else {
                Application.Quit();
            }
        }
    }

    // ##########################################################################################
    private Vector3 TouchPoint() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // ------------------------------------------------------------------------------------------
    public RaycastHit2D[] Touch2D( TouchPhase touchPhase ) {
        List<RaycastHit2D>  result  =   new List<RaycastHit2D>();
        for ( var i = 0; i < Input.touchCount; ++i ) {
            if ( Input.GetTouch(i).phase == touchPhase ) {
                Vector3         hitPoint    =   Camera.main.ScreenToWorldPoint( Input.GetTouch(i).position );
                RaycastHit2D    hitInfo     =   Physics2D.Raycast( hitPoint, Vector2.zero );
                result.Add( hitInfo );
            }
        }
        return result.ToArray();
    }



    // ------------------------------------------------------------------------------------------
    public void OnTouchObject() {
        foreach( var touch in Touch2D( TouchPhase.Began ) ) {
            if ( touch ) {
                GameObject hit = touch.transform.gameObject;
                if ( hit.tag == "Hittable" ) {
                    Destroy(hit);
                    points++;
                    GameObject.Find("Points Text").GetComponent<Text>().text = "Twoje próby: " + points.ToString();
                } else if ( hit.tag == "HitEnd") {
                    SceneManager.LoadScene( SceneManager.GetActiveScene().name );
                }
            }
        }
    }

    // ##########################################################################################
    private void MoveCamera() {
        //  Touch Start
        if (Input.GetMouseButtonDown(0)) {
            touchStart  =   TouchPoint();
            GameObject.Find("Title Text").GetComponent<Text>().text = "";
        }
        //  Touch End
        if (Input.GetMouseButton(0)) {
            Vector3 direction               =   touchStart - TouchPoint();
            Camera.main.transform.position  =   Tools.ClampMove(
                Camera.main.transform.position, direction, moveBorders
            );
        }
    }

    // ------------------------------------------------------------------------------------------
    private bool PinchToZoom() {
        if ( Input.touchCount == 2 ) {
            Touch   touchOne            =   Input.GetTouch(0);
            Touch   touchTwo            =   Input.GetTouch(1);
            Vector2 touchOnePrevPos     =   touchOne.position - touchOne.deltaPosition;
            Vector2 touchTwoPrevPos     =   touchTwo.position - touchTwo.deltaPosition;
            float   previousMagnitude   =   (touchOnePrevPos - touchTwoPrevPos).magnitude;
            float   currentMagnitude    =   (touchOne.position - touchTwo.position).magnitude;
            float   difference          =   currentMagnitude - previousMagnitude;
            Zoom( difference );
            return true;
        }
        return false;
    }

    // ------------------------------------------------------------------------------------------
    void Zoom( float increment ) {
        float   diffrence               =   zoomSpeed * Time.deltaTime * (increment < 0 ? -1 : 1);
        float   newSize                 =   Mathf.Clamp(
            Camera.main.orthographicSize - diffrence, zoomOutMin, zoomOutMax );
        Camera.main.orthographicSize    =   newSize;
    }

    // ##########################################################################################
}
