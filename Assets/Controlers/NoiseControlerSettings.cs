using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
public class NoiseControlerSettings : ScriptableObject {


  public bool lock_x_y_scale = true;

  [Range(0f,30f)]
  public float x_scale;
  [Range(0f,30f)]
  public float y_scale;

  [Range(2,200)]
  public int x_res;
  [Range(2,200)]
  public int y_res;


  public float getXScale() {
    return x_scale;
  }

  public float getYScale() {
    if(lock_x_y_scale) {
      return x_scale;
    } 
    return y_scale;
  }

  public NoiseControlerSettings(float x_s, float y_s, int x_r, int y_r) {
    x_scale = x_s;
    y_scale = y_s;

    x_res = x_r;
    y_res = y_r;

    lock_x_y_scale = false;
  }

}
