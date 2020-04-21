using System.Collections;
using UnityEngine;
using UnityEditor;

public class HelixCon : INoiseCon {

  public override void OnValidate() {
    Debug.Log("Noise Controler: OnValidate");

    if(generator == null) {
      generator = new RidgeGen2D();
      //generator = new Perlin2DGenerator();
      generator.newNoise(noise_con_set);
    }

    viewer = GetComponent<NoiseViewer>();
    viewer.setNoiseController(this);
    sendNoiseToViewer();
  }

  protected override void sendNoiseToViewer() {
    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    for(int i = 0; i < noise_con_set.x_res; i++) {

      float y_0 = generator.sample(new float[] { i * x_scale, 0});
      ns.set(new int[] {i, 0}, y_0);

      for(int j = 1; j < noise_con_set.y_res; j++) {

        float v = generator.sample(new float[] { i * x_scale, j * y_scale}) * (1 - j / (noise_con_set.y_res - 1f)) + (j / (noise_con_set.y_res - 1f)) * y_0;
        ns.set(new int[] { i,j}, v);
      }
    }

    viewer.setNoiseStore(ns);

  }

}
