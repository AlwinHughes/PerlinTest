using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTest : MonoBehaviour {

  [SerializeField]
  public CurveChain chain;

  [SerializeField]
  public GameObject[] gos;

  //[SerializeField]
  //public GameObject[] tang gos;

  private int num_marker = 10;

  public void OnValidate() {

    Debug.Log("chain test on validate");

    int c = chain.getLength();
    if(gos == null || gos.Length != chain.getLength() * num_marker) {
      gos = new GameObject[num_marker*c];
    }

    chain.align();

    ICurve cur;

    float t = 0f;

    for(int i = 0; i < c; i++) {

      cur = chain.getCurve(i);

      Debug.Log("start: " + cur.position(cur.min));
      Debug.Log("end: " + cur.position(cur.max));

      for(int j = 0; j < num_marker; j++) {

        if(gos[i * num_marker + j] == null) {
          gos[i * num_marker + j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        t = lerp(cur.min, cur.max, j/( (float) num_marker ));

        Vector3[] vs = cur.all(t);

        gos[i*num_marker+ j].transform.position = vs[0];

        gos[i*num_marker + j].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);


        Debug.DrawRay(vs[0], vs[1].normalized, Color.red, 5f);

        Debug.DrawRay(vs[0], vs[2].normalized, Color.green, 5f);
      }
      
    }
  }

  private float lerp(float a, float b, float t) {
    return (1 - t) * a + t * b;
  }

}
