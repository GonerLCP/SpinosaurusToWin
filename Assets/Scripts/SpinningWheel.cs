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
    public event Action SmallJumpAction;
    public event Action AccelAction;
    public event Action ScreamingAction;

    public GameObject SpinoVectorUpReference;

    public AudioClip screamSound;
    public AudioClip wheelSound;

    AudioSource WheelAudioSource;

    public SpriteChanger spriteChanger;

    public bool IsPlayerDead;
    bool CdFinished;
    bool respinningWheelBool;

    public float WheelCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wheeltransform = GetComponent<Transform>();
        WheelAudioSource = GetComponent<AudioSource>();
        WheelAudioSource.clip = wheelSound;
        IsPlayerDead = false;
        CdFinished = true;
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
            ActualRotationValue += RotationDecreaseValue;
        }
        else if (ActualRotationValue <= -RotationDecreaseValue)
        {
            ActualRotationValue -= RotationDecreaseValue;
        }
        else {ActualRotationValue =0f; }

        if (Input.GetKeyDown("space") && IsPlayerDead == false && CdFinished ==true)
        {
            ActualRotationValue = 0f;

            angleBetweenVectors = AngleEn360(SpinoVectorUpReference.transform.up, transform.up, Vector3.forward);
            if (angleBetweenVectors < FirstAngle)
            {
                print("1");
                BigJumpAction?.Invoke();
            }
            else if (angleBetweenVectors  < SecondAngle)
            {
                print("zebi ça marche");
                AccelAction?.Invoke();
            }
            else if (angleBetweenVectors < ThirdAngle)
            {
                print("2");
                SmallJumpAction?.Invoke();
            }
            else
            {
                SoundFXManager.Instance.PlaySoundFXClip(screamSound, transform,0.2f);
                StartCoroutine(ScreamingChangeSprite());
                print("3");
                ScreamingAction?.Invoke();
            }
            if (respinningWheelBool)
            {
                StartCoroutine(RespinningWheel());
            }
            StartCoroutine(WheelCooldownCoroutine());
        }

        if (Mathf.Abs(ActualRotationValue) >0)
        {
            WheelAudioSource.mute = false;
            float t = Mathf.Abs(ActualRotationValue) / 20;
            t = t > 1 ? 1 : t;
            WheelAudioSource.pitch = Mathf.Lerp(0.3f, 1.5f,t);
        }
        else
        {
            WheelAudioSource.mute = true;
        }
    }

    IEnumerator RespinningWheel()
    {
        yield return new WaitForSeconds(0.2f);
        ActualRotationValue += RotationValue *10* UnityEngine.Random.Range(-1, 2);
    }

    IEnumerator WheelCooldownCoroutine()
    {
        CdFinished = false;
        yield return new WaitForSeconds(WheelCooldown);
        CdFinished = true;
    }

    IEnumerator ScreamingChangeSprite()
    {
        spriteChanger.ChangeSprite(PlayerState.Screaming);
        yield return new WaitForSeconds(1.5f);
        spriteChanger.ChangeSprite(PlayerState.BaseSpinoWoSkate);
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
