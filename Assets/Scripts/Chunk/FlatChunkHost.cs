using System.Collections;
using System;
using UnityEngine;

public class FlatChunkHost : IChunkHost<FlatChunk, NoiseGen1Geom>  {


  [SerializeField]
  public GeomNoiseSettings geom_noise_set;

  override protected NoiseGen1Geom getNoiseGen(int i, int j) {
    return new NoiseGen1Geom(
        new NoiseSetting(noise_set, new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0)),
        geom_noise_set);
  }

}
