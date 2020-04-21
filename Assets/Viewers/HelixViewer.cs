using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixViewer : NoiseViewer {

  private int[] triangles;
  private int[] reverse_triangles;
  private Vector3[] verts;

  [SerializeField]
  private GameObject mesh_obj;
  private MeshFilter mesh_filter;

  private GameObject reverse_mesh_obj;
  private MeshFilter reverse_mesh_filter;

  [SerializeField]
  public NoiseStore noise_store;

  [Range(-20f, 20f)]
  public float hold_radius = 1f;

  [Range(-100f, 20f)]
  public float thick_radius = 1f;

  [Range(-100f, 100f)]
  public float y_max = 1f;

  [Range(-20f, 100f)]
  public float length = 1f;

  public void OnValidate() {
    if(mesh_obj == null || reverse_mesh_obj == null) {
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

    //reverse mesh
    reverse_mesh_obj = new GameObject("reverse mesh");
    reverse_mesh_obj.transform.parent = transform;

    reverse_mesh_obj.AddComponent<MeshRenderer>().sharedMaterial =  new Material(Shader.Find("Standard"));

    reverse_mesh_filter = reverse_mesh_obj.AddComponent<MeshFilter>();
    reverse_mesh_filter.mesh = new Mesh();
  }

  private void constructMesh() {
    Debug.Log("construct mesh");
    if(noise_store == null) {
      Debug.Log("noise store null");
      return;
    }

    verts = new Vector3[noise_store.storeLength()];

    triangles = new int[(noise_store.getDims()[0] - 1) * (noise_store.getDims()[1] - 1) *6];

    reverse_triangles = new int[(noise_store.getDims()[0] - 1) * (noise_store.getDims()[1] - 1) *6];

    //theta is always in the same plane. 
    float theta;
    //phi rotates to form the shell of the donut
    float phi;
    Vector3 r;
    int vert_index;
    int tri_index = 0;

    for(int i = 0; i < noise_store.getDims()[0]; i++) {
      theta = length * i / (noise_store.getDims()[0] - 1f );

      //Debug.Log("theta : " + theta);

      r = new Vector3( hold_radius * Mathf.Sin(theta), 0f, hold_radius * Mathf.Cos(theta));

      for(int j = 0; j < noise_store.getDims()[1]; j++) {

        vert_index = noise_store.getStoreIndex(new int[] {i,j});
        phi = 2f * Mathf.PI * j / (noise_store.getDims()[1] - 1f );

        //verts[vert_index] = r + thick_radius * (new Vector3(- Mathf.Cos(phi) * Mathf.Sin(theta), Mathf.Cos(phi), -Mathf.Cos(phi) * Mathf.Cos(theta)));
        verts[vert_index] = new Vector3(
            (hold_radius + (thick_radius + noise_store.get(new int[] {i,j})) * Mathf.Cos(phi)) *  Mathf.Sin(theta),
            (thick_radius + noise_store.get(new int[] {i,j})) * Mathf.Sin(phi) + y_max * i / (noise_store.getDims()[0] - 1f ),
            (hold_radius + (thick_radius + noise_store.get(new int[] {i,j})) * Mathf.Cos(phi)) *  Mathf.Cos(theta)
            );


        if(i != (noise_store.getDims()[0] -1) && j != (noise_store.getDims()[1] -1)){

          triangles[tri_index] = vert_index;
          triangles[tri_index + 1] = vert_index + noise_store.getDims()[0];
          triangles[tri_index + 2] = vert_index + noise_store.getDims()[0] + 1;

          triangles[tri_index + 3] = vert_index;
          triangles[tri_index + 4] = vert_index + noise_store.getDims()[0] + 1;
          triangles[tri_index + 5] = vert_index + 1;

          //reverse triangles
          reverse_triangles[tri_index] = vert_index;
          reverse_triangles[tri_index + 1] = vert_index + noise_store.getDims()[0] + 1;
          reverse_triangles[tri_index + 2] = vert_index + noise_store.getDims()[0];

          reverse_triangles[tri_index + 3] = vert_index;
          reverse_triangles[tri_index + 4] = vert_index + 1;
          reverse_triangles[tri_index + 5] = vert_index + noise_store.getDims()[0] + 1;

          tri_index += 6;
        }
      }
    }

    this.mesh_filter.sharedMesh.Clear();
    this.mesh_filter.sharedMesh.vertices = verts;
    this.mesh_filter.sharedMesh.triangles = triangles;
    this.mesh_filter.sharedMesh.RecalculateNormals();

    this.reverse_mesh_filter.sharedMesh.Clear();
    this.reverse_mesh_filter.sharedMesh.vertices = verts;
    this.reverse_mesh_filter.sharedMesh.triangles = reverse_triangles;
    this.reverse_mesh_filter.sharedMesh.RecalculateNormals();
  }

  public override void setNoiseStore(NoiseStore ns) {
    this.noise_store = ns;
    constructMesh();
  }

  public void refreshNoise() {
    controller.refreshNoise();
  }




}
