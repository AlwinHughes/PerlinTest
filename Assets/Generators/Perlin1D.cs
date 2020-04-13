using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin1D : ScriptableObject, NoiseGenerator {

  public Perlin1D() {
    perlin = new Perlin2DGenerator();
  }

  private Perlin2DGenerator perlin;

  public void newNoise(NoiseControlerSettings settings) {
    perlin.newNoise(settings);
  }

  public float sample(float f) {
    return perlin.sample(new Vector2(0f, f));
  }

  public float sample(float[] f) {
    return perlin.sample(new Vector2(0f, f[0]));
  }

  public bool isReady() { return perlin.isReady(); }

}
