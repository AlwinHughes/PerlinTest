﻿using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridNoiseViewer))]
public class NoiseViewerEditor : Editor {

  /*
  private bool ns_fold_out = true;
  private Editor ns_editor;
  */

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    GridNoiseViewer nv = (GridNoiseViewer) target;

    DrawSettingsEditor(nv.controller.getSettings(), nv.controller.onSettingsChanged, ref nv.controller.getFoldout(), ref nv.controller.getEditor());

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
