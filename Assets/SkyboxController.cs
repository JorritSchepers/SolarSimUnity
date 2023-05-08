using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public int totalStars = 100;
    public int distance = 100000;
    public int minSize = 1;
    public int maxSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomStars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRandomStars()
    {
        for (int i = 0; i < totalStars; i++)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            star.transform.rotation = Random.rotation;

            var size = Random.Range(minSize, maxSize);
            star.transform.localScale = new Vector3(size, size, size);

            star.transform.parent = transform;
            star.GetComponent<Renderer>().material.color = Color.white;
            star.transform.Translate(Vector3.forward * distance);
        }
    }
}
