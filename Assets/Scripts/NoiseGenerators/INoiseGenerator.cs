using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class INoiseGenerator {

  abstract public float sample2D(float x, float y);

  abstract public float sample3D(float x, float y, float z);

  public NoiseSetting noise_set;
}
