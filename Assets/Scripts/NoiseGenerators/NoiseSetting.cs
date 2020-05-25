using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseSetting : ScriptableObject {
    
    [Range(0f, 20f)]
    public float scale_x;

    [Range(0f, 20f)]
    public float scale_y;

    [Range(0f, 20f)]
    public float scale_z;

    public Vector3 offset = new Vector3();
}
