using UnityEngine;
using System;

[CreateAssetMenu()]
[Serializable]
public class TunnelSettings : ScriptableObject {

  public CurveChain chain;

  [SerializeField]
  public NoiseControlerSettings[] noise_settings;

}
