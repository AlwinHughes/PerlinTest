﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : ICurve {

  //defines a plane that the vector is in
  //public Vector3 normal;

  

  public Parabola() {
  }
  //unfinished
  
  protected override Vector3 pos(float t) {
    return new Vector3();
  }

  public override Vector3[] all(float t) {
    return new Vector3[1];
  }

  protected override Vector3 tan(float t) {
    return new Vector3();
  }

  

}
