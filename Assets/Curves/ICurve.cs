using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICurve {

  public float min;
  public float max;

  abstract public Vector3 pos(float t);

  abstract public Vector3 normal(float t);

}
