using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint m_ConfigurableJoint;
    Quaternion targetIntialRotation;
    // Start is called before the first frame update
    void Start()
    {
        m_ConfigurableJoint = GetComponent<ConfigurableJoint>();
        targetIntialRotation = targetLimb.transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_ConfigurableJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation()
    {
        return Quaternion.Inverse(targetLimb.localRotation) * targetIntialRotation;
    }
}
