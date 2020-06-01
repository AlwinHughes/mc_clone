using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockyV13DChunkHost : IChunkHost<Blocky3DChunk, NoiseGen2> {

  /*
  [SerializeField]
  public GeomNoiseSettings geom_noise_set;
  */

  override protected NoiseGen2 getNoiseGen(int i, int j) {
    return new NoiseGen2(
      new NoiseSetting(
        noise_set,
        new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0)
      )
    );
  }

}
