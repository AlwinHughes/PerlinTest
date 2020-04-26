using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Helix : ICurve {

  [Range(-20f,20f)]
  public float a;
  [Range(-20f,20f)]
  public float b;
  [Range(-20f,20f)]
  public float h;

  public Helix() {
    min = 0;
    max = 2 * Mathf.PI;
    a = 1f;
    b = 1f;
    h = 1f;
  }

  public Helix(float a, float b, float h) {
    min = 0;
    max = Mathf.PI;
    this.a = a;
    this.b = b;
    this.h = h;
  }

  protected override Vector3 pos(float t) {
    if(base.loop && t > max) {
      t = t % max;
    }
    return new Vector3(Mathf.Sin(t) * a, t * h, Mathf.Cos(t) * b);
  }

  protected override Vector3 tan(float t) {
    if(base.loop && t > max) {
      t = t % max;
    }
    return new Vector3(Mathf.Cos(t) * a, h, -Mathf.Sin(t) * b);
  }


}
