using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Event Ãß°¡
public enum EVENT_TYPE {
    NONE, 

};

public interface IListener {
    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null);
}
