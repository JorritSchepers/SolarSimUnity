using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public int totalStars = 100;
    public int distance = 100000;
    public int minSize = 1;
    public int maxSize = 5;
    public int factor = 6;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomStars();
    }

    void GenerateRandomStars()
    {
        for (int i = 0; i < totalStars; i++)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            var ran = Random.rotation;

            ran.x /= factor;
            ran.z /= factor;

            star.transform.rotation = ran;

            var size = Random.Range(minSize, maxSize);
            star.transform.localScale = new Vector3(size, size, size);

            star.transform.parent = transform;
            star.GetComponent<Renderer>().material.color = Color.white;
            star.transform.Translate(Vector3.forward * distance);
        }

        transform.Rotate(new Vector3(0, 0, 45));
    }
}
