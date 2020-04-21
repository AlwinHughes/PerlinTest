using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : ICurve {

  //defines a plane that the vector is in
  public Vector3 normal;

  

  public Parabola() {
  }
  //unfinished
  
  public override Vector3 pos(float t) {
    return null;
  }

  public override Vector3 normal(float t) {
    return new Vector3(0,1,0);
  }

}
