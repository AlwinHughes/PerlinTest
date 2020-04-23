using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingCon3 : INoiseCon {

  public override void OnValidate() {
    if(generator == null) {
      //generator = new RidgeGen2D();
      //generator = new Perlin2DGenerator();
      generator.newNoise(noise_con_set);
    }

    viewer = GetComponent<NoiseViewer>();
    viewer.setNoiseController(this);
    sendNoiseToViewer();
  }

  public override void sendNoiseToViewer() {

    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    /*
    for(int i = 0; i < noise_con_set.x_res; i++) {

      float y_0 = generator.sample(new float[] { i * x_scale, 0});
    }
    */

  }
}
