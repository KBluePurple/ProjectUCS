using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSystem {
    float Health { get; }
    float GetNormalizedHealth();
}