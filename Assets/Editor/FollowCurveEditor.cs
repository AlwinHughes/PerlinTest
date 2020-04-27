using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FolowCurve))]
public class FollowCurveEditor : Editor {

  Editor follow_curve_editor;
  bool follow_curve_foldout = true;

  Editor curve_editor;
  bool curve_foldout = true;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    FolowCurve fc = (FolowCurve) target;

    DrawSettingsEditor(fc.settings, fc.onSettingsChanged, ref follow_curve_foldout, ref follow_curve_editor);

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
