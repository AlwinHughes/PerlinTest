using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICurve : ScriptableObject {

  public float min;
  public float max;

  abstract public Vector3 pos(float t);

  abstract public Vector3 tangent(float t);

  virtual public Vector3 normal(float t) {
    return Vector3.Cross(pos(t), tangent(t));
  }

  //returns the position, the tangent and a vector normal to the curve
  virtual public Vector3[] all(float t) {
    Vector3 v = pos(t);
    Vector3 u = tangent(t);

    return new Vector3[] {v, u, Vector3.Cross(v,u).normalized }; 
  }

  public bool loop = false;

}
