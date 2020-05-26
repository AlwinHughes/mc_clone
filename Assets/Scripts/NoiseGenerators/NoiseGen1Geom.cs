using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGen1Geom : INoiseGenerator {

  public GeomNoiseSettings geom_nois_set;

  public NoiseGen1Geom(NoiseSetting ns) {
    noise_set = ns;
  }

  public NoiseGen1Geom(NoiseSetting ns, GeomNoiseSettings gns) {
    noise_set = ns;
    geom_nois_set = gns;
  }

  public override float sample2D(float x, float y) {
    if(geom_nois_set == null) {
      geom_nois_set = new GeomNoiseSettings();
    }
    float res = 0;
    float r = 1;
    float s = 0;
    for(int i = 0; i < geom_nois_set.layers; i++) {
      res += Mathf.PerlinNoise(
          r * (x * noise_set.scale_x + noise_set.offset.x),
          r * (y * noise_set.scale_y + noise_set.offset.y)
          ) / r ;
      s += r;
      r *= geom_nois_set.ratio;
    }
    return res;

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
