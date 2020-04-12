using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoiseViewer : MonoBehaviour {

  public INoiseCon controller;

  public void setNoiseController(INoiseCon con) {
    this.controller = con;
  }

  abstract public void setNoiseStore(NoiseStore store);
}
