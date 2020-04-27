using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TorusViewer))]
public class TorusViewerEditor : Editor {

  Editor editor;
  bool fold_out;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    TorusViewer nv = (TorusViewer) target;

    DrawSettingsEditor(nv.controller.noise_con_set, nv.controller.onSettingsChanged, ref fold_out, ref editor);

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
