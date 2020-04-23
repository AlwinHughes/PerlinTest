using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Perlin3DCon : INoiseCon {

  [Range(0f,10f)]
  public float z_slice = 0;

  public override void OnValidate() {

    Debug.Log("Noise Controler: OnValidate");

    if(generator == null) {
      generator = new Perlin3DGenerator();
      generator.newNoise(noise_con_set);
    }

    viewer = GetComponent<NoiseViewer>();
    viewer.setNoiseController(this);
    Debug.Log("is view null: " + viewer == null);
    sendNoiseToViewer();
  }


  public override void sendNoiseToViewer() {
    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    for(int i = 0; i < noise_con_set.x_res; i++) {
      for(int j = 0; j < noise_con_set.y_res; j++) {

        ns.set(new int[] { i,j}, generator.sample(new float[] { i * x_scale, j * y_scale, z_slice}));

      }
    }

    viewer.setNoiseStore(ns);

  }

}
