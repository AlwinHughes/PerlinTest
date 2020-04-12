using System.Collections;
using UnityEngine;
using UnityEditor;

public interface INoiseCon {

  void refreshNoise();

  NoiseControlerSettings getSettings();

  ref Editor getEditor();

  ref bool getFoldout();

  void onSettingsChanged();

}
