﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ridge3DCon : MonoBehaviour, INoiseCon {

  private bool con_foldout = true;
  public ref bool getFoldout() { return ref con_foldout; }

  private Editor con_editor;
  public ref Editor getEditor() { return ref con_editor; }

  [SerializeField]
  public NoiseGenerator generator;

  [SerializeField]
  public NoiseControlerSettings noise_con_set;

  public NoiseControlerSettings getSettings() { return noise_con_set; }

  [Range(0f,20f)]
  public float z_slice = 0;

  [Range(0f,10f)]
  public float z_scale = 1f;

  private NoiseViewer viewer;

  public void OnValidate() {

    Debug.Log("Noise Controler: OnValidate");

    if(generator == null) {
      generator = new RidgeGen3D();
      generator.newNoise(noise_con_set);
    }

    viewer = GetComponent<NoiseViewer>();
    viewer.setNoiseController(this);
    Debug.Log("is view null: " + viewer == null);
    sendNoiseToViewer();
  }


  public void refreshNoise() {
    generator.newNoise(noise_con_set);
    sendNoiseToViewer();
  }

  public void onSettingsChanged() {
    sendNoiseToViewer();
  }

  private void sendNoiseToViewer() {
    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    for(int i = 0; i < noise_con_set.x_res; i++) {
      for(int j = 0; j < noise_con_set.y_res; j++) {

        ns.set(new int[] { i,j}, generator.sample(new float[] { i * x_scale, j * y_scale, z_slice * z_scale}));

      }
    }

    viewer.setNoiseStore(ns);

  }

}
