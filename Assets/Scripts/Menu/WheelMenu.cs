using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelMenu : MonoBehaviour
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


    public GameObject SpinoVectorUpReference;

    public AudioClip screamSound;

    AudioSource WheelAudioSource;

    public GameObject CreditPanel;

    public float WheelCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wheeltransform = GetComponent<Transform>();
        WheelAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        angleBetweenVectorsTemp = AngleEn360(SpinoVectorUpReference.transform.up,transform.up, Vector3.forward);
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ActualRotationValue -= RotationValue;
            print("up");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ActualRotationValue += RotationValue;
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
            if (angleBetweenVectors < FirstAngle || angleBetweenVectors>FourthAngle)
            {
                SoundFXManager.Instance.PlaySoundFXClip(screamSound, transform, 0.2f);
            }
            else if (angleBetweenVectors  < SecondAngle)
            {
                Application.Quit();
            }
            else if (angleBetweenVectors < ThirdAngle)
            {
                CreditPanel.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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
        Gizmos.color = Color.orange;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -FirstAngle) * Vector3.up * 5f);
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -SecondAngle) * Vector3.up * 5f);
        Gizmos.color = Color.pink;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -ThirdAngle) * Vector3.up * 5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -FourthAngle) * Vector3.up * 5f);
    }

    }
