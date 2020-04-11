using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class RidgeGen2D : ScriptableObject, NoiseGenerator{

  public RidgeGen2D() {
    perlin = new Perlin2DGenerator();
  }

  public Perlin2DGenerator perlin;

  public void newNoise(NoiseControlerSettings settings) {
    perlin.newNoise(settings);
  }

  public float sample(float[] arr) {
    return Mathf.Pow(1 - Mathf.Abs(perlin.sample(arr)),2);
  }

  public bool isReady() { return perlin.isReady(); }

}
