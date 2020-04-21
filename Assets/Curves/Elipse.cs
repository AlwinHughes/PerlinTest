using System;
using UnityEngine;

[Serializable]
public class Circle : ICurve {

  public float a;
  public float b;

  public Circle() {
    min = 0;
    max = Mathf.PI;
    a = 1f;
    b = 1f;
  }

  public Circle(float a, float b) {
    min = 0;
    max = 2f * Mathf.PI;
    this.a = a;
    this.b = b;
  }

  public override Vector3 pos(float t) {
    return new Vector3(Mathf.Sin(t), 0, Mathf.Cos(t));
  }

  public override Vector3 normal(float t) {
    return new Vector3(0,1,0);
  }
}
