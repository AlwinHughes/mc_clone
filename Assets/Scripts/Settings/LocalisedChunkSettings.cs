using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LocalisedChunkSettings : ScriptableObject {

  [Range(1f, 100f)]
  public float range = 5f;
}
