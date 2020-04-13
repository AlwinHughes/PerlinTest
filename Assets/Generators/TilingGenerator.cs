using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class TilingGenerator : ScriptableObject, NoiseGenerator {

  public TilingGenerator() {
    perlin = new Perlin2DGenerator();
  }

  private Perlin2DGenerator perlin;

  [SerializeField]
  public float[] edge_noise;

  private NoiseControlerSettings noise_set;

  public void newNoise(NoiseControlerSettings settings) {
    perlin.newNoise(settings);
    noise_set = settings;
    getEdgeNoise();
  }

  public void updateSettings(NoiseControlerSettings set) {
    this.noise_set = set;
    getEdgeNoise();
  }

  private void getEdgeNoise() {

    edge_noise = new float[noise_set.x_res];
    float inv_res = noise_set.getYScale() / (noise_set.y_res - 1f);

    for(int i = 0; i < noise_set.x_res; i++) {
      edge_noise[i] = sample(new float[] { i * inv_res, 0f});
      //Debug.Log("edge noise: " + i * inv_res);
    }


  }

  public float sample(Vector2 v) {
    //max value for x will be: 
    float max = noise_set.getYScale();

    float to_index = (noise_set.y_res - 1f) / noise_set.getYScale();

    return (1 - v.y / max) * perlin.sample(v) + v.y * edge_noise[(int) (v.x * to_index)] / max;
  }

  public float sample(float[] arr) {
    return sample(new Vector2(arr[0], arr[1]));
  }

  public bool isReady() { return perlin.isReady(); }


}
