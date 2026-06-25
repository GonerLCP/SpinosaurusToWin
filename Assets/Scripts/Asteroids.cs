using System.Collections;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject asteroidsGO;
    public float horizontalOffsetMin = 10f;
    public float horizontalOffsetMax = 15f;

    public float verticalOffsetMin = 10f;
    public float verticalOffsetMax = 15f;

    public int minTime;
    public int maxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RainingDown());
    }

    IEnumerator RainingDown()
    {
        int rand = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(rand);
        float spawnX = transform.position.x + Random.Range(horizontalOffsetMin, horizontalOffsetMax);
        float spawnY = transform.position.y + Random.Range(verticalOffsetMin, verticalOffsetMax);

        Instantiate(asteroidsGO,new Vector3(spawnX, spawnY, 3f),Quaternion.identity);
        StartCoroutine(RainingDown());
    }
}
