using System.Collections;
using System;
using UnityEngine;

public interface NoiseGenerator {

  void newNoise(NoiseControlerSettings settings);

  float sample(float[] arr);

  //int[] getDims();

  bool isReady();
}
