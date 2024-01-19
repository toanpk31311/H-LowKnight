using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpikeMovement : MonoBehaviour
{

    private Vector3 iniPosition;
    private Vector3 targetPosition;
    public float t;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Awake()
    {
        iniPosition = transform.position;
        targetPosition = new Vector3(width, height, 0f);
    }

    // Update is called once per frame
    void Update()
    { 
        StartCoroutine(moving());
    }

    private IEnumerator moving(){
        if(transform.position == iniPosition){
            yield return new WaitForSeconds(3);
            moveUp();
            if(transform.position == targetPosition){
                StopAllCoroutines();
            }
        }
        if(transform.position == targetPosition){
            yield return new WaitForSeconds(3);
            moveDown();
            if(transform.position == iniPosition){
                StopAllCoroutines();
            }
        }
    }

    private void moveUp(){
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        Debug.Log("Moving up");
    }

    private void moveDown(){
        transform.position = Vector3.Lerp(transform.position, iniPosition, t);
        Debug.Log("Moving down");
    }

}
