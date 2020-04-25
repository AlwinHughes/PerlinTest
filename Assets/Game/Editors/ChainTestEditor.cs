using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChainTest))]
public class ChainTestEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    ChainTest ct = (ChainTest) target;

    if(GUILayout.Button("Re-Draw objects")){
      ct.OnValidate();
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
