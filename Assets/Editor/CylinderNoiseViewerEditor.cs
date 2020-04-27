using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CylinderNoiseViewer))]
public class CylinderNoiseViewerEditor : Editor {

  Editor noise_editor;
  bool noise_foldout = true;

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    CylinderNoiseViewer nv = (CylinderNoiseViewer) target;

    DrawSettingsEditor(nv.controller.noise_con_set, nv.controller.onSettingsChanged, ref noise_foldout, ref noise_editor);

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
