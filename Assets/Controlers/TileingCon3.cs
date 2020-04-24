using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileingCon3 : INoiseCon {

  public override void OnValidate() {
    if(generator == null) {
      //generator = new RidgeGen2D();
      generator = new Perlin2DGenerator();
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

    //value of the noise at 0,0
    float origin = generator.sample(new float[] {0,0});

    for(int i = 0; i < noise_con_set.x_res; i++) {

      //gets the value of the noise at (i * x_scale, 0) on the x axis
      float y_0 = lerp(generator.sample(new float[] { i * x_scale, 0}), origin, i / (noise_con_set.x_res- 1f));
     
      //float y_0 = generator.sample(new float[] { i * x_scale, 0});

      //sets the noise at (i,0) to be equal to y_0 to make it match
      ns.set(new int[] {i, 0}, y_0);

      for(int j = 0; j < noise_con_set.y_res; j++) {
        /*
        float v = lerp(
            generator.sample(new float[] { i * x_scale, j * y_scale}),
            y_0,
            j / (noise_con_set.y_res - 1f)
        );
        

        //v = lerp(v, ns.get(new int[] { 0, j}), i / (noise_con_set.x_res - 1f));
        */

        

        ns.set(new int[] {i,j},
            (1 - weight(new Vector2(i / (noise_con_set.x_res- 1f), j / (noise_con_set.y_res- 1f))))
            * generator.sample(new float[] { i* x_scale, j * y_scale})
            );



        //ns.set(new int[] {i,j}, weight(0f, 0f, new Vector2(i / (noise_con_set.x_res- 1f), j / (noise_con_set.y_res- 1f))));

      }

      viewer.setNoiseStore(ns);

    }
  }

  private float lerp(float a, float b, float t) {
    return a * (1 - t) + b * t;
  }

  private float weight(Vector2 t) {
    float p = 2f;
    return Mathf.Pow(Mathf.Max(
      Mathf.Max(Mathf.Pow(t.x,p), Mathf.Pow(t.y,p)),
      Mathf.Max(Mathf.Pow(1-t.x,p), Mathf.Pow(1 - t.y,p))
    ), p);
  }
}
