using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurveChain : MonoBehaviour {


  public bool spin_camer = true;
  [Range(0f, 100f)]
  public float spin_speed;
  private float camer_spin_t = 0f;


  [Range(-10f, 10f)]
  public float t_offset = 0f;
  private float t = 0f;
  [Range(-10f, 10f)]
  public float speed;

  public CurveChain chain;

  void Start() { }

  void Update() {
    transform.position = chain.position(t);
    t += speed * Time.deltaTime;

    Vector3 tangent = chain.tangent(t + t_offset);

    transform.rotation = Quaternion.LookRotation(tangent);

    if(spin_camer) {
      
      transform.rotation = Quaternion.AngleAxis(camer_spin_t, tangent) * transform.rotation;
      camer_spin_t += Time.deltaTime * spin_speed;
    }
  }
}
