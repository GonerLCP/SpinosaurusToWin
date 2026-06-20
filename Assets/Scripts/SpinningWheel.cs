using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class SpinningWheel : MonoBehaviour
{
    Transform Wheeltransform;
    public float RotationValue;
    public float RotationDecreaseValue;
    public float ActualRotationValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wheeltransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ActualRotationValue += RotationValue;
            print("up");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ActualRotationValue -= RotationValue;
            print("down");
        }

        Wheeltransform.rotation = Wheeltransform.rotation * Quaternion.Euler(0f, 0f, ActualRotationValue);

        if (ActualRotationValue >= RotationDecreaseValue)
        {
            ActualRotationValue -= RotationDecreaseValue;
        }
        else if (ActualRotationValue <= -RotationDecreaseValue)
        {
            ActualRotationValue += RotationDecreaseValue;
        }
        else {ActualRotationValue =0f; }

        if (Input.GetKeyDown("space"))
        {
            ActualRotationValue = 0f;
        }
    }
}
