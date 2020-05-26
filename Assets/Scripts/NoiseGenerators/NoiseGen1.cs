using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGen1 : INoiseGenerator {

  public NoiseGen1(NoiseSetting ns) {
    noise_set = ns;
  }

  public override float sample2D(float x, float y) {
    return Mathf.PerlinNoise(
        x * noise_set.scale_x + noise_set.offset.x,
        y * noise_set.scale_y + noise_set.offset.y
        );
  }

  public override float sample3D(float x, float y, float z) {

    float xy = Mathf.PerlinNoise(noise_set.scale_x * x + noise_set.offset.x, noise_set.scale_y * y + noise_set.offset.y);
    float xz = Mathf.PerlinNoise(noise_set.scale_x * x+ noise_set.offset.x, noise_set.scale_z * z + noise_set.offset.z);
    float yz = Mathf.PerlinNoise(noise_set.scale_y * y + noise_set.offset.y,noise_set.scale_z *  z+ noise_set.offset.z);
    float yx = Mathf.PerlinNoise(noise_set.scale_y * y + noise_set.offset.y, noise_set.scale_x * x+ noise_set.offset.x);
    float zx = Mathf.PerlinNoise(noise_set.scale_z * z+ noise_set.offset.z, noise_set.scale_x * x+ noise_set.offset.x);
    float zy = Mathf.PerlinNoise(noise_set.scale_z *z+ noise_set.offset.z, noise_set.scale_y * y + noise_set.offset.y);
    return xy + xz + yz + yx + zx + zy;
  }



}
