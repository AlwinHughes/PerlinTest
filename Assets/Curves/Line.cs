using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Line : ICurve {

  public Vector3 start;
  public Vector3 tan_vec;

  public Line(Vector3 start, Vector3 tan) {
    this.start = start;
    this.tan_vec = tan;
  }

  protected override Vector3 pos(float t) {
    return start + t * tan_vec;
  }

  protected override Vector3 tan(float t) {
    return tan_vec;
  }

}
