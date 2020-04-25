using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTest : MonoBehaviour {

  public Vector3 d;

  public void OnValidate() {


  Debug.DrawLine(transform.position, transform.position + d, Color.white, 5f, false);

  Debug.DrawLine(transform.position, transform.position + transform.up, Color.red, 10f);

    transform.rotation = Quaternion.LookRotation(d, Vector3.up);
  }
}
