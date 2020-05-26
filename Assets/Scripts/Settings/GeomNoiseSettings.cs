using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GeomNoiseSettings : ScriptableObject {

  [Range(1, 10)]
  public int layers;

  [Range(1f, 10f)]
  public float ratio;

  public GeomNoiseSettings() {
    layers = 3;
    ratio = 5f;
  }
}
