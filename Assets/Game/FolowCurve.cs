using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCurve : MonoBehaviour {
  // Start is called before the first frame update
  

  private float t = 0f;
  public CurveFolowSet settings;

  void Start() {
    transform.position = settings.curve.pos(t);
    transform.rotation = Quaternion.LookRotation(settings.curve.tangent(t));
  }

  // Update is called once per frame
  void Update() {
    t += settings.speed * Time.deltaTime;
    if(!settings.curve.loop) 
      t = Mathf.Min(settings.curve.max, t);

    transform.position = settings.offset + settings.curve.pos(t);
    transform.rotation = Quaternion.LookRotation(settings.curve.tangent(t));
  }
}
