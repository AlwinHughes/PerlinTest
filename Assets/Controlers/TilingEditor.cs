using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilingCon))]
public class TilingEditor :Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    TilingCon tc = (TilingCon) target;

    
    DrawSettingsEditor((ScriptableObject) tc.generator, tc.onSettingsChanged, ref tc.getFoldout(), ref tc.getEditor());

    /*
    if(GUILayout.Button("refreshNoise")){
      nv.refreshNoise();
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
