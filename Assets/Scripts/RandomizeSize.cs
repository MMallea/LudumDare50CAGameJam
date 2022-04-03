using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSize : MonoBehaviour
{
    public float minSize = 1;
    public float maxSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        float size = Random.Range(minSize, maxSize);
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
}
