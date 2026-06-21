using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class SpinningWheel : MonoBehaviour
{
    Transform Wheeltransform;
    public float RotationValue;
    public float RotationDecreaseValue;
    public float ActualRotationValue;

    public float FirstAngle;
    public float SecondAngle;
    public float ThirdAngle;
    public float FourthAngle;

    public float angleBetweenVectors;
    public float angleBetweenVectorsTemp;

    public event Action BigJumpAction;

    public GameObject SpinoVectorUpReference;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wheeltransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        angleBetweenVectorsTemp = AngleEn360(SpinoVectorUpReference.transform.up,transform.up, Vector3.forward);
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

            angleBetweenVectors = AngleEn360(SpinoVectorUpReference.transform.up, transform.up, Vector3.forward);
            if (angleBetweenVectors < FirstAngle)
            {
                print("ouais le jump");
                BigJumpAction?.Invoke();
            }
            else if (angleBetweenVectors  < SecondAngle)
            {
                print("ouais le hurlement");
            }
            else if (angleBetweenVectors < ThirdAngle)
            {
                print("ouais le ollie");
            }

        }
    }

    private float AngleEn360(Vector3 from,Vector3 to, Vector3 direction)
    {
        float angleSigned = Vector3.SignedAngle(from, to, direction);
        if (angleSigned <0)
        {
            return angleSigned + 360;
        }
        return angleSigned;
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.orange;
        //Gizmos.DrawRay(SpinoVectorUpReference.transform.position, SpinoVectorUpReference.transform.up.normalized);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, Vector3.up * 5f);
        Vector3 VectorUpRefWheel = transform.up - transform.position;
        Gizmos.color = Color.orange;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -FirstAngle) * Vector3.up * 5f);
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -SecondAngle) * Vector3.up * 5f);
        Gizmos.color = Color.pink;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -ThirdAngle) * Vector3.up * 5f);


    }
    }
