using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : ICurve {

  public float a;
  public float b;
  public float h;

  public Helix() {
    min = 0;
    max = Mathf.PI;
    a = 1f;
    b = 1f;
  }

  public Helix(float a, float b, float h) {
    min = 0;
    max = Mathf.PI;
    this.a = a;
    this.b = b;
    this.h = h;
  }

  public override Vector3 pos(float t) {
    return new Vector3(Mathf.Sin(t) * a, t * h, Mathf.Cos(t) * b);
  }

  public override Vector3 normal(float t) {
    return Vector3.Cross(pos(t), new Vector3(Mathf.Sin(t) * a, t * h, Mathf.Cos(t) * b)).normalized;
  }
}
