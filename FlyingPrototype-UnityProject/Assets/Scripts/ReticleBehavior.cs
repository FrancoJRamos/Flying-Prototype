using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleBehavior : MonoBehaviour
{
    //Working from video: https://www.youtube.com/watch?v=-bkmPm_Besk

    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Find mouse position on the screen
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //store change between mouse position and gameObject position as vector3
        Vector3 rotation = mousePos - transform.position;

        //get the angle in radians by using inverser tan of vector.y component over vector.x component and convert to degrees
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //rotate game object(and its children) by the angle found
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        //Debug.Log(rotZ);

    }

    
}
