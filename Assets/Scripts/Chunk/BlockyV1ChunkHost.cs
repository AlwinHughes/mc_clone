using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockyV1ChunkHost :IChunkHost<BlockChunkV1, NoiseGen1Geom> {

  [SerializeField]
  public GeomNoiseSettings geom_noise_set;


  void Start() {

  }

  // Update is called once per frame
  void Update()
  {

  }


  override protected NoiseGen1Geom getNoiseGen(int i, int j) {
    return new NoiseGen1Geom(
        new NoiseSetting(noise_set, new Vector3(i * noise_set.scale_x, j * noise_set.scale_y,0)),
        geom_noise_set);
  }

}
