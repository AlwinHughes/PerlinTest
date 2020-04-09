using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class PerlinNoise : ScriptableObject {

  [SerializeField]
  public int width;
  [SerializeField]
  public int height;

  [SerializeField]
  private Vector2[] grads;

  public PerlinNoise(int width, int height) {
    this.width = width;
    this.height = height;
    grads = new Vector2[width * height];
    generateGrads();
  }

  public bool isReady() { return grads != null; }

  public void generateGrads() {
    Debug.Log("generating grads");
    float f = 0;
    for(int i = 0; i < width * height; i++) {
      f = UnityEngine.Random.Range(0f, 1f);
      grads[i] = new Vector2(f, 1f - f);
    }
  }

  private float lerp(float a, float b, float t) {
    return b * t + a * (1- t);
  }

  public float sample(float i, float j) {
    return sample(new Vector2(i,j));
  }

  public float sample(Vector2 point) {
    int x =  ((int) Mathf.Floor(point.x)) % (width -1);
    float dx = point.x - x;

    int y = ((int) Mathf.Floor(point.y)) % (height -1);
    float dy = point.x - y;

    float top_left_dot = Vector2.Dot(point, grads[x + width * y]);
    float top_right_dot = Vector2.Dot(point, grads[x+1 + width * y]);
    float bottom_left_dot = Vector2.Dot(point, grads[x + width * (y+1)]);
    float bottom_right_dot = Vector2.Dot(point, grads[x+1 + width * (y+1)]);

    float x1 = lerp(top_left_dot, top_right_dot, dx);
    float x2 = lerp(bottom_left_dot, bottom_right_dot, dx);

    return lerp(x1, x2, dy);
  }



}
