using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgeGen3D : ScriptableObject, NoiseGenerator {

  public RidgeGen3D() {
    perlin = new Perlin3DGenerator();
  }

  private Perlin3DGenerator perlin;

  public void newNoise(NoiseControlerSettings settings) {
    perlin.newNoise(settings);
  }

  public float sample(float[] arr) {
    return Mathf.Pow(1 - Mathf.Abs(perlin.sample(arr)),2);
  }

  public float sample(Vector3 point) {
    return Mathf.Pow(1 - Mathf.Abs(perlin.sample(point)),2);
  }

  public bool isReady() { return perlin.isReady(); }

}
