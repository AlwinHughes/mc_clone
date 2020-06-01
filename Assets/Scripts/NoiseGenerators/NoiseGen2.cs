using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGen2 : INoiseGenerator {

  private FastNoise fn;

  public NoiseGen2(NoiseSetting ns) {
    Debug.Log("noise gen 2 constructor");
    noise_set = ns;
    fn = new FastNoise();
    fn.SetNoiseType(FastNoise.NoiseType.PerlinFractal);
    fn.SetFractalOctaves(3);
    fn.SetFractalLacunarity(2f);
    fn.SetFractalGain(0.5f);
    fn.SetFrequency(1f);
  }

  public override float sample2D(float x, float y) {
    return fn.GetNoise(x + noise_set.offset.x, y + noise_set.offset.y,0);
  }

  public override float sample3D(float x, float y, float z) {

    return fn.GetNoise(noise_set.scale_x * x + noise_set.offset.x , noise_set.scale_y *y + noise_set.offset.y , noise_set.scale_z * z + noise_set.offset.z );
  }

}
