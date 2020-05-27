using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlockyV1Settings : ScriptableObject {

  [Range(1f, 100f)]
  public float steps = 1;

  public BlockyV1Settings(float s) {
    steps = s;
  }

public BlockyV1Settings() {
  steps = 10f;
}

}
