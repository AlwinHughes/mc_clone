using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockyV3ChunkHost : IChunkHost<BlockyChunkV3, BlockyGen1> {

  [SerializeField]
  public GeomNoiseSettings geom_noise_set;

  public BlockyV1Settings blocky_set;

  override protected BlockyGen1 getNoiseGen(int i, int j) {
    return new BlockyGen1(
        new NoiseSetting(noise_set, new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0)),
        geom_noise_set,
        blocky_set);
  }

}
