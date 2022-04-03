using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveContinuous : MonoBehaviour
{
    public bool randDir;
    public Vector2 speedMultiplierRange;
    public Vector3 velocity = Vector3.zero;

    [Header("Model Rotation")]
    public bool modelFaceTowardsDir;
    public GameObject modelObj;

    private Rigidbody rBody;

    // Start is called before the first frame update
    protected void Start()
    {
        rBody = GetComponent<Rigidbody>();
        if(randDir)
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            velocity = new Vector3(dir.x, 0, dir.y);
        }

        if(modelFaceTowardsDir)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity, Vector3.up);
            modelObj.transform.rotation = rotation;
        }
    }

    private void FixedUpdate()
    {
        rBody.position += (velocity * Random.Range(speedMultiplierRange.x, speedMultiplierRange.y));
    }
}
