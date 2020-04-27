using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CurveViewer))]
public class CurveViewerEditor : Editor {

  Editor curve_editor;
  bool curve_foldout = true;

  Editor noise_editor;
  bool noise_foldout = true;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    CurveViewer nv = (CurveViewer) target;

    DrawSettingsEditor(nv.controller.noise_con_set, nv.controller.onSettingsChanged, ref noise_foldout, ref noise_editor);

    DrawSettingsEditor(nv.curve, nv.controller.onSettingsChanged, ref curve_foldout, ref curve_editor);


    if(GUILayout.Button("refreshNoise")){
      nv.refreshNoise();
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
