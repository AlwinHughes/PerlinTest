using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CurveChain : ScriptableObject {

  [SerializeField]
  private List<ICurve> curves = new List<ICurve>();

  public CurveChain() {}

  public ICurve getCurve(int i) {
    if(i < curves.Count) {
      return curves[i];
    }
    return null;
  }

  public int getLength() {
    return curves.Count;
  }

  public void addCurve(ICurve c) {
    curves.Add(c);
  }

  public Vector3 position(double t) {
    double acc = 0d;
    for(int i = 0; i < curves.Count; i++) {
      if(t > curves[i].min && t < acc + curves[i].max) {
        return curves[i].position((float) (t - acc));
      }
      acc += curves[i].max;
    }
    //after the end of the curve
    return new Vector3();
  }

  public Vector3 tangent(double t) {
    double acc = 0d;
    for(int i = 0; i < curves.Count; i++) {
      if(t > curves[i].min && t < acc + curves[i].max) {
        return curves[i].tangent((float) (t - acc));
      }
      acc += curves[i].max;
    }
    //after the end of the curve
    return new Vector3();
  }

  //ensures that each of the curves starts where the last one ends
  //will also later ensure that the tangets match
  public void align() {

    Quaternion r = new Quaternion();;
    for(int i = 1; i < curves.Count; i++) {

      curves[i].offset = new Vector3(0,0,0);
      curves[i].offset = curves[i-1].position(curves[i-1].max) - curves[i].position(curves[i].min);

      curves[i].rotation = new Quaternion();
      curves[i].rotation = Quaternion.FromToRotation(curves[i].tangent(curves[i].min), curves[i-1].tangent(curves[i-1].max));
    }
  }

  public void align(bool align_normals) {

    if(!align_normals) {
      align();
      return;
    }

    //Quaternion r = new Quaternion();;
    for(int i = 1; i < curves.Count; i++) {

      curves[i].offset = new Vector3(0,0,0);
      curves[i].offset = curves[i-1].position(curves[i-1].max) - curves[i].position(curves[i].min);

      curves[i].rotation = new Quaternion();

      /*curves[i].rotation = Quaternion.FromToRotation(
          curves[i].normal(curves[i].min),
          curves[i-1].normal(curves[i-1].max)
          )

        * */ 
      curves[i].rotation = Quaternion.FromToRotation(
            curves[i].tangent(curves[i].min),
            curves[i-1].tangent(curves[i-1].max)
            );


       // curves[i].rotation = Quaternion.AngleAxis(curves[i].normal_angle, curves[i].tangent(curves[i].min)) * curves[i].rotation;


    }
  }


}
