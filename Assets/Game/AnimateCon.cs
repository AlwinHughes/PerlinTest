using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCon : MonoBehaviour {


  [Range(0f,5f)]
  public float speed = 0.1f;

  public INoiseCon con;

  private IAnimate anim_con;
  void Start() {
    con = GetComponent<INoiseCon>();
    anim_con = (IAnimate) con;
    if(anim_con == null) {
      Debug.Log("not animatable");
    }
  }

  void Update() {
    if(anim_con != null) {
      anim_con.setParam(anim_con.getParam() + Time.deltaTime * speed);
      con.onSettingsChanged();

    }
  }
}
