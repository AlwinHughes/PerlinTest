using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class Tunnel : MonoBehaviour {

  public TunnelSettings tunnel_settings;

  [SerializeField]
  private GameObject[] curve_objects;
  //public CurveViewer[] curve_viewers;

  public void OnValidate() {
    if(tunnel_settings.chain != null && tunnel_settings != null) {


      if(curve_objects == null || curve_objects.Length == 0)
        createTunnel();

      //drawTunnel();
    }
  }

  private void createTunnel() {
    Debug.Log("create tunnel");

    tunnel_settings.chain.align(true);
    if(curve_objects == null || curve_objects.Length != tunnel_settings.chain.getLength()) {
      curve_objects = new GameObject[tunnel_settings.chain.getLength()];
    }

    for(int i = 0; i < tunnel_settings.chain.getLength(); i++) {
      
      curve_objects[i] = new GameObject("curve");
      
      CurveViewer cv = curve_objects[i].AddComponent<CurveViewer>();

      cv.curve = tunnel_settings.chain.getCurve(i);
      cv.r_offset = tunnel_settings.radial_offset;

      INoiseCon nc = curve_objects[i].AddComponent<INoiseCon>();

      nc.noise_con_set = tunnel_settings.noise_settings[i];

      nc.onSettingsChanged();

    }

  }

  private void updateTunnel() {
    Debug.Log("draw tunnel");
      
    tunnel_settings.chain.align(true);
    for(int i = 0; i < tunnel_settings.chain.getLength(); i++) {

      CurveViewer cv = curve_objects[i].GetComponent<CurveViewer>();
      cv.curve = tunnel_settings.chain.getCurve(i);
      cv.r_offset = tunnel_settings.radial_offset;

      INoiseCon nc = curve_objects[i].GetComponent<INoiseCon>();
      nc.noise_con_set = tunnel_settings.noise_settings[i];
      nc.onSettingsChanged();
    }


  }

  public void onSettingsChange() {
    updateTunnel();
  }

}
