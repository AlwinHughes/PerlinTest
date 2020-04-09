using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoiseController))]
public class NoiseControllerEditor : Editor {


  private bool gen_foldout = true;
  private Editor gen_editor;

  private bool con_foldout = true;
  private Editor con_editor;

  private NoiseController nc;
  

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    nc = (NoiseController) target;

    DrawSettingsEditor(nc.noise_con_set,nc.onSettingsChanged , ref con_foldout, ref con_editor);

    if(GUILayout.Button("refreshNoise")){
      nc.refreshNoise();
    }

    /*
    if(GUILayout.Button("view noise")){
      nc.refreshNoise();
    }
    */

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
