using System.Collections;
using UnityEngine;

public class NoiseViewer : MonoBehaviour
{

  private int[] triangles;
  private Vector3[] verts;

  public INoiseCon controller;

  [SerializeField]
  private GameObject mesh_obj;
  private MeshFilter mesh_filter;

  [SerializeField]
  public NoiseStore noise_store;
  public bool ns_fold_out;

  public void OnValidate() {
    Debug.Log("on OnValidate");
    if(mesh_obj == null) {
      createMesh();
    }

    constructMesh();
  }

  private void createMesh() {
    mesh_obj = new GameObject("mesh");
    mesh_obj.transform.parent = transform;

    mesh_obj.AddComponent<MeshRenderer>().sharedMaterial =  new Material(Shader.Find("Standard"));

    mesh_filter = mesh_obj.AddComponent<MeshFilter>();
    mesh_filter.mesh = new Mesh();
  }


  public void setNoiseStore(NoiseStore ns) {
    this.noise_store = ns;
    constructMesh();
  }

  private void constructMesh() {


    if(noise_store == null) {
      Debug.Log("noise store null");
      return;
    }
    //asuming square and 2d for the moment
    int res = noise_store.getDims()[0];

    verts = new Vector3[noise_store.storeLength()];

    triangles = new int[(noise_store.getDims()[0] - 1) * (noise_store.getDims()[1] - 1) *6];

    int vert_index = 0;
    int tri_index = 0;

    for(int i = 0; i < noise_store.getDims()[0]; i++) {
      for(int j = 0; j < noise_store.getDims()[1]; j++) {

        vert_index = noise_store.getStoreIndex(new int[] {i,j});

        verts[vert_index] = new Vector3(i / (noise_store.getDims()[0] - 1f ), noise_store.get(new int[] {i,j}), j / (noise_store.getDims()[1] - 1f ));


        if(i != (noise_store.getDims()[0] -1) && j != (noise_store.getDims()[1] -1)){

          triangles[tri_index] = vert_index;
          triangles[tri_index + 1] = vert_index + noise_store.getDims()[0];
          triangles[tri_index + 2] = vert_index + noise_store.getDims()[0] + 1;

          triangles[tri_index + 3] = vert_index;
          triangles[tri_index + 4] = vert_index + noise_store.getDims()[0] + 1;
          triangles[tri_index + 5] = vert_index + 1;

          tri_index += 6;
        }
      }
    }

    this.mesh_filter.sharedMesh.Clear();
    this.mesh_filter.sharedMesh.vertices = verts;
    this.mesh_filter.sharedMesh.triangles = triangles;
    this.mesh_filter.sharedMesh.RecalculateNormals();


  }

  private float dotProd(float[] a, float[] b) {
    float ret = 0;
    for(int i = 0; i < a.Length; i++) {
      ret = a[i] * b[i];
    }
    return ret;
  }

  public void setNoiseController(INoiseCon controller) {
   this.controller = controller;
  }

  public void refreshNoise() {
    controller.refreshNoise();
  }
}
