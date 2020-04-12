using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu()]
[Serializable]
public class Perlin3DGenerator : ScriptableObject, NoiseGenerator {

  [SerializeField]
  private Vector3[] grads;

  public bool isReady() { return grads != null; }

  public int height = 8;
  public int width = 8;
  public int depth = 8;

  public void newNoise(NoiseControlerSettings settings) {
    Debug.Log("generating grads");
    grads = new Vector3[height * width * depth];
    float f = 0;
    for(int i = 0; i < width * height * depth; i++) {
      f = UnityEngine.Random.Range(0f, 1f);
      grads[i] = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
      //grads[i].Normalize();
    }
  }

  public float sample(float[] arr) {
    return sample(new Vector3(arr[0],arr[1], arr[2]));
  }

  public float sample(Vector3 point) {
    int x =  ((int) point.x) % (width );
    float dx = point.x - Mathf.Floor(point.x);

    int y = ((int) point.y) % (height );
    float dy = point.y - Mathf.Floor(point.y);
    
    int z = ((int) point.z) % (depth );
    float dz = point.z - Mathf.Floor(point.z);

    point = new Vector3(fade(dx), fade(dy), fade(dz));
    //point = new Vector3(dx, dy, dz);

    //top things

    float front_top_left_dot = Vector3.Dot(point, grads[getIndex(x,y,z)]);

    float front_top_right_dot = Vector3.Dot(point - new Vector3(1,0,0), grads[getIndex(x+1,y,z)]);

    float front_bottom_left_dot = Vector3.Dot(point - new Vector3(0,1,0), grads[getIndex(x, y+1, z)]);

    float front_bottom_right_dot = Vector3.Dot(point - new Vector3(1,1,0), grads[getIndex(x+1, y+1, z)]);

    float x1 = lerp(front_top_left_dot, front_top_right_dot, fade(dx));
    float x2 = lerp(front_bottom_left_dot, front_bottom_right_dot, fade(dx));
    /*
    float x1 = lerp(front_top_left_dot, front_top_right_dot, dx);
    float x2 = lerp(front_bottom_left_dot, front_bottom_right_dot, dx);
    */

    // bottom things

    float back_top_left_dot = Vector3.Dot(point - new Vector3(0,0,1), grads[getIndex(x,y,z+1)]);

    float back_top_right_dot = Vector3.Dot(point - new Vector3(1,0,1), grads[getIndex(x+1,y,z+1)]);

    float back_bottom_left_dot = Vector3.Dot(point - new Vector3(0,1,1), grads[getIndex(x, y+1, z + 1)]);

    float back_bottom_right_dot = Vector3.Dot(point - new Vector3(1,1,1), grads[getIndex(x+1, y+1, z+1)]);

    float y1 = lerp(back_top_left_dot, back_top_right_dot, fade(dx));
    float y2 = lerp(back_bottom_left_dot, back_bottom_right_dot, fade(dx));
    /*
    float y1 = lerp(back_top_left_dot, back_top_right_dot, dx);
    float y2 = lerp(back_bottom_left_dot, back_bottom_right_dot, dx);
    */

    float z1 = lerp(x1,x2, fade(dy));
    float z2 = lerp(y1, y2, fade(dy));
    
    /*
    float z1 = lerp(x1,x2, dy);
    float z2 = lerp(y1, y2, dy);
    */

    return lerp(z1, z2, fade(dz));
  }

  private int getIndex(int x, int y, int z) {
    return (x % height) + height * (y % width)+ width * height * (z % depth);
  }


  private float lerp(float a, float b, float t) {
    return b * t + a * (1- t);
  }

  
  private float fade(float t) {
    return t * t * t * (t * (t * 6 - 15) + 10);
  }
  

  private float time_fade(float t) {
    return t * t * t * t;
  }

  /*
  private float fade(float t) {
    //return 0.5f * (Mathf.Sin(Mathf.PI * (t - 0.5f)) + 1f);
    return t * t * t * t;
  }
  */

  /*
  public int[] getDims() {
  }
  */

}
