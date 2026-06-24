using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startingPos;
    private float lengthOfSprite;
    public float AmountOfParallax;
    public Camera MainCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = transform.position.x;

        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - AmountOfParallax);
        float Distance = Position.x * AmountOfParallax;

        Vector3 NewPosition = new Vector3(startingPos + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;

        if (Temp > startingPos + (lengthOfSprite / 2))
        {
            startingPos += lengthOfSprite;
        }
        else if (Temp < startingPos - (lengthOfSprite / 2))
        {
            startingPos -= lengthOfSprite;
        }
    }
}
