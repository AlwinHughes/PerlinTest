using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu()]
public class Elipse : ICurve {

  public float a;
  public float b;

  public Elipse() {
    min = 0;
    max = Mathf.PI;
    a = 1f;
    b = 1f;
  }

  public Elipse(float a, float b) {
    min = 0;
    max = 2f * Mathf.PI;
    this.a = a;
    this.b = b;
  }

  protected override Vector3 pos(float t) {
    return new Vector3(Mathf.Sin(t) * a , 0, Mathf.Cos(t) * b );
  }


  protected override Vector3 tan(float t) {
    return new Vector3(Mathf.Cos(t) * a , 0, -Mathf.Sin(t) * b );
  }

  //returns the position, the tangent and a vector normal to the curve
  /*
  public override Vector3[] all(float t) {
    return new Vector3[] { pos(t), tangent(t), new Vector3(0,1,0)};
  }

  public override Vector3 normal(float t) {
    return new Vector3(0,1,0);
  }
  */
}
