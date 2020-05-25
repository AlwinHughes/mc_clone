using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour {

  private FlatChunk fc;

  public Vector3 direction;

  void Start() {
    fc = GetComponent<FlatChunk>();
  }

  void Update() {
    fc.noise_set.offset += (Time.deltaTime * direction);
    fc.onNoiseSetChange();
  }
}
