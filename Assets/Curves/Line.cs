using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Line : ICurve {

  public Vector3 start;
  public Vector3 tan;

  public Line(Vector3 start, Vector3 tan) {
    this.start = start;
    this.tan = tan;
  }

  public override Vector3 pos(float t) {
    return start + t * tan;
  }

  public override Vector3 tangent(float t) {
    return tan;
  }
}
