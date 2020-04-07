using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu()]
[Serializable]
public class NoiseStore : ScriptableObject {

  [SerializeField]
  private float[] store;

  [SerializeField]
  private int[] dims;

  [SerializeField]
  private int rank;

  [SerializeField]
  private int[] dims_mul;

  public NoiseStore(){}

  public NoiseStore(int[] dims) {
    this.dims = dims;
    this.store = new float[storeLength()];
    rank = dims.Length;
    computeDimsMul();
  }

  private void computeDimsMul() {
    dims_mul = new int[dims.Length];
    int acc = 1;
    for(int i = 0; i < dims_mul.Length; i++) {
      dims_mul[i] = acc;
      acc *= dims[i];
    }
  }

  public void zero() {
    this.store = new float[storeLength()];
  }

  public int storeLength() {
    int ret = 1;
    for(int i = 0; i < dims.Length; i++) {
      ret *= dims[i];
    }
    return ret;
  }

  public int[] getDims() { return dims; }

  public int getStoreIndex(int[] item) {
    if(item.Length != dims.Length) {
      throw new Exception("dimention mismatch");
    }

    int ret = 0; 
    for(int i = 0; i < item.Length; i++) {
      ret += item[i] * dims_mul[i];
    }
    return ret;
  }

  public float get(int[] index) {
    return store[getStoreIndex(index)];
  }

  public void set(int[] index, float val) {
    store[getStoreIndex(index)] = val;
  }

  public bool isReady() {
    return store != null;
  }

  public float getMin() {
    float min = store[0];
    for(int i = 0; i < store.Length; i++) {
      if(store[i] < min) {
        min = store[i];
      }
    }
    return min;
  }

  public float getMax() {
    float max = store[0];
    for(int i = 0; i < store.Length; i++) {
      if(store[i] > max) {
        max = store[i];
      }
    }
    return max;
  }

}
