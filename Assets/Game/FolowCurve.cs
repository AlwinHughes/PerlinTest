using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCurve : MonoBehaviour {
  // Start is called before the first frame update
  

  public bool spin_camer = true;

  [Range(0f, 100f)]
  public float spin_speed;

  private float camer_spin_t = 0f;

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

    Vector3 tangent = settings.curve.tangent(t);

    transform.position = settings.offset + settings.curve.pos(t) - (settings.tangent_offset * tangent);
    transform.rotation = Quaternion.LookRotation(settings.curve.tangent(t));

    if(spin_camer) {
//Quaternion.AngleAxis(phi
      //transform.rotation = Quaternion.AngleAxis(spin_speed , tangent) * transform.rotation;
      //spin_speed += Time.deltaTime;
      
      transform.rotation = Quaternion.AngleAxis(camer_spin_t, tangent) * transform.rotation;
      camer_spin_t += Time.deltaTime * spin_speed;
    }
  }

  public void onSettingsChanged() {
  }
}
