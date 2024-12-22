using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static bool LayerInMask(int layer, LayerMask mask) {
        return mask == (1 << layer);
    }
}
