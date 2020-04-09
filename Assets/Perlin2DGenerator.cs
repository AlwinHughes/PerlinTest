using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Perlin2DGenerator : ScriptableObject, NoiseGenerator {

  [SerializeField]
  private Vector2[] grads;

  public bool isReady() { return grads != null; }

  public int height = 10;
  public int width = 10;

  public void newNoise(NoiseControlerSettings settings) {
    Debug.Log("generating grads");
    grads = new Vector2[height * width];
    float f = 0;
    for(int i = 0; i < width * height; i++) {
      f = UnityEngine.Random.Range(0f, 1f);
      grads[i] = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
      //grads[i].Normalize();
    }
  }

  public float sample(float[] arr) {
    return sample(new Vector2(arr[0],arr[1]));
  }

  public float sample(Vector2 point) {
    int x =  ((int) point.x) % (width -1);
    float dx = point.x - Mathf.Floor(point.x);

    int y = ((int) point.y) % (height -1);
    float dy = point.y - Mathf.Floor(point.y);

    point = new Vector2(fade(dx), fade(dy));

    float top_left_dot = Vector2.Dot(point, grads[x + width * y]);
    float top_right_dot = Vector2.Dot(point - new Vector2(1,0), grads[x+1 + width * y]);
    float bottom_left_dot = Vector2.Dot(point - new Vector2(0,1), grads[x + width * (y+1)]);
    float bottom_right_dot = Vector2.Dot(point - new Vector2(1,1), grads[x+1 + width * (y+1)]);

    float x1 = lerp(top_left_dot, top_right_dot, fade(dx));
    float x2 = lerp(bottom_left_dot, bottom_right_dot, fade(dx));

    return lerp(x1, x2, fade(dy));
  }


  private float lerp(float a, float b, float t) {
    return b * t + a * (1- t);
  }

  private float fade(float t) {
    return t * t * t * (t * (t * 6 - 15) + 10);
  }

  /*
  public int[] getDims() {
  }
  */

}
