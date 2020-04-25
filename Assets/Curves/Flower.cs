using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Flower : ICurve {

  [Range(-10f,10f)]
  public float a;

  [Range(-10f,10f)]
  public float r;

  public Flower() {
    a = 1f;
    min = 0;
    max = 2 * Mathf.PI;
  }

  [Range(0f,10f)]
  public float h = 1f;


  protected override Vector3 pos(float t) {
    //goes up then down
    //return r * new  Vector3(Mathf.Sin(t * a) * Mathf.Sin(t), h * Mathf.Sin(0.5f * t), Mathf.Sin(a * t) * Mathf.Cos(t));
    

    //just goew up
    return r * new  Vector3(Mathf.Sin(t * a) * Mathf.Sin(t), h * t, Mathf.Sin(a * t) * Mathf.Cos(t));
  }

  protected override Vector3 tan(float t) {
    return r * new Vector3( 
        a * Mathf.Cos(a * t) * Mathf.Sin(t) + Mathf.Cos(t) * Mathf.Sin(t), 
        h,
        //h * Mathf.Cos(0.5f * t) * 0.5f,
        a * Mathf.Cos(a * t) * Mathf.Cos(t) + Mathf.Sin(a * t) * Mathf.Sin(t));
  }
}
