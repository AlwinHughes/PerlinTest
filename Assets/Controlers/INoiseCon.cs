﻿using System.Collections;
using UnityEngine;

public abstract class INoiseCon : MonoBehaviour {

  [SerializeField]
  public NoiseGenerator generator;

  [SerializeField]
  public NoiseControlerSettings noise_con_set;

  protected NoiseViewer viewer;

  abstract public void OnValidate();

  public virtual void refreshNoise() {
    generator.newNoise(noise_con_set);
    sendNoiseToViewer();
  }

  public virtual void onSettingsChanged() {
    sendNoiseToViewer();
  }

  abstract protected void sendNoiseToViewer(); /*{
    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    for(int i = 0; i < noise_con_set.x_res; i++) {
      for(int j = 0; j < noise_con_set.y_res; j++) {

        ns.set(new int[] { i,j}, generator.sample(new float[] { i * x_scale, j * y_scale}));

      }
    }

    viewer.setNoiseStore(ns);

  }
  */

}
