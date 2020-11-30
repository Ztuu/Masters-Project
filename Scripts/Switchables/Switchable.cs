using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Switchable: Resettable
{
    bool GetCurrentState();
    void Switch();
    void Switch(bool targetState);
}
