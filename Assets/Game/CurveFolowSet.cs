using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu()]
public class CurveFolowSet : ScriptableObject {

  public ICurve curve;
  public Vector3 offset;

  [Range(-5f,5f)]
  public float tangent_offset;

  [Range(0f,5f)]
  public float speed;

}
