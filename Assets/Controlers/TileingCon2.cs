using System.Collections;
using UnityEngine;
using UnityEditor;

public class TileingCon2 : MonoBehaviour, INoiseCon{

  private bool con_foldout = true;
  public ref bool getFoldout() { return ref con_foldout; }

  private Editor con_editor;
  public ref Editor getEditor() { return ref con_editor; }

  [SerializeField]
  public NoiseGenerator generator;

  [SerializeField]
  public NoiseControlerSettings noise_con_set;

  public NoiseControlerSettings getSettings() { return noise_con_set; }

  private NoiseViewer viewer;

  public void OnValidate() {

    Debug.Log("Noise Controler: OnValidate");

    if(generator == null) {
      //generator = new RidgeGen2D();
      generator = new Perlin2DGenerator();
      generator.newNoise(noise_con_set);
    }

    viewer = GetComponent<NoiseViewer>();
    viewer.setNoiseController(this);
    sendNoiseToViewer();
  }

  public void refreshNoise() {
    generator.newNoise(noise_con_set);
    sendNoiseToViewer();
  }

  public void onSettingsChanged() {
    sendNoiseToViewer();
  }

  private void sendNoiseToViewer() {
    NoiseStore ns = new NoiseStore(new int[] {noise_con_set.x_res, noise_con_set.y_res});

    float x_scale = noise_con_set.getXScale() / noise_con_set.x_res;
    float y_scale = noise_con_set.getYScale() / noise_con_set.y_res;

    for(int i = 0; i < noise_con_set.x_res; i++) {

      float y_0 = generator.sample(new float[] { i * x_scale, 0});
      ns.set(new int[] {i, 0}, y_0);

      for(int j = 1; j < noise_con_set.y_res; j++) {

        float v = generator.sample(new float[] { i * x_scale, j * y_scale}) * (1 - j / (noise_con_set.y_res - 1f)) + (j / (noise_con_set.y_res - 1f)) * y_0;

        ns.set(new int[] { i,j}, v);

      }
    }

    viewer.setNoiseStore(ns);

  }


}
