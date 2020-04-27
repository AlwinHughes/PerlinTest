using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilingCon))]
public class TilingEditor :Editor {

  Editor editor;
  bool fold_out;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    TilingCon tc = (TilingCon) target;

    DrawSettingsEditor((ScriptableObject) tc.generator, tc.onSettingsChanged, ref fold_out, ref editor);

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
