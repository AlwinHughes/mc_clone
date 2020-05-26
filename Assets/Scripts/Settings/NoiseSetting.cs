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

    public NoiseSetting(float s_x, float s_y, float s_z, Vector3 off) {
      scale_x = s_x;
      scale_y = s_y;
      scale_z = s_z;
      offset = off;
    }

    public NoiseSetting(NoiseSetting ns, Vector3 exta_offset) {

      scale_x = ns.scale_x;
      scale_y = ns.scale_y;
      scale_z = ns.scale_z;

      offset = ns.offset + exta_offset;
    }
}
