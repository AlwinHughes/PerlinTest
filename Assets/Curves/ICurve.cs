using System;
using UnityEngine;


//to do: sub classes override a different position method and it is only abstract classes pos method that adds the offset
[Serializable]
public abstract class ICurve : ScriptableObject {

  public float min;
  public float max;

  public Vector3 offset = new Vector3();

  public Quaternion rotation = new Quaternion();

  abstract protected Vector3 pos(float t);
  abstract protected Vector3 tan(float t);

  public Vector3 position(float t) {
    //Debug.Log("offset : " + offset);
    //return  offset + (rotation * (pos(t) - pos(0)));
    return  offset + (rotation * (pos(t) - pos(0)));
  }



  public Vector3 tangent(float t) {
    return rotation * tan(t);
  }


  virtual public Vector3 normal(float t) {
    return Vector3.Cross(position(t), tangent(t)).normalized;
  }

  //returns the position, the tangent and a vector normal to the curve
  virtual public Vector3[] all(float t) {
    Vector3 v = position(t);
    Vector3 u = tangent(t);

    return new Vector3[] {v, u, Vector3.Cross(v,u).normalized }; 
  }

  public bool loop = false;

}
