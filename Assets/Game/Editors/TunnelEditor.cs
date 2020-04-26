using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tunnel))]
public class TunnelEditor : Editor {

  Editor[] curve_editors;
  bool[] curve_foldouts;

  Editor[] curve_noise_editors;
  bool[] curve_noise_foldouts;

  Editor tunnel_editor;
  bool tunnel_foldout;

  public override void OnInspectorGUI() { 
    DrawDefaultInspector();

    Tunnel t = (Tunnel) target;
    //DrawSettingsEditor(t.tunnel_settings, t.onSettingsChange, ref tunnel_foldout, ref tunnel_editor);

    if(t.tunnel_settings.noise_settings != null && t.tunnel_settings.chain != null) {

      if(curve_editors == null || curve_editors.Length != t.tunnel_settings.chain.getLength()) {
        curve_editors = new Editor[t.tunnel_settings.chain.getLength()];
        curve_foldouts = new bool[t.tunnel_settings.chain.getLength()];

      }

      if(curve_noise_editors == null || curve_noise_editors.Length != t.tunnel_settings.chain.getLength()) {
        curve_noise_editors = new Editor[t.tunnel_settings.chain.getLength()];
        curve_noise_foldouts = new bool[t.tunnel_settings.chain.getLength()];
      }


      for(int i = 0; i < t.tunnel_settings.chain.getLength(); i++) {
        DrawSettingsEditor(t.tunnel_settings.noise_settings[i], t.onSettingsChange, ref curve_noise_foldouts[i], ref curve_noise_editors[i]);
        DrawSettingsEditor(t.tunnel_settings.chain.getCurve(i), t.onSettingsChange, ref curve_foldouts[i], ref curve_editors[i]);
      }


    }

  }

  void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
  {
    if (settings != null)
    {
      foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
      using (var check = new EditorGUI.ChangeCheckScope())
      {
        if (foldout)
        {
          CreateCachedEditor(settings, null, ref editor);
          editor.OnInspectorGUI();

          if (check.changed)
          {
            if (onSettingsUpdated != null)
            {
              onSettingsUpdated();
            }
          }
        }
      }
    }
  }


}
