using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChunkSettings : ScriptableObject {

  [Range(0,100)]
  public int res_x;
  [Range(0,100)]
  public int res_y;

  [Range(1f,20f)]
  public float x_length;
  [Range(1f,20f)]
  public float y_length;
}

