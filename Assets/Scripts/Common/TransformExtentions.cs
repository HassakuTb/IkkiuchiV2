using UnityEngine;

public static partial class TransformExtentions {

    public static void DestroyAllChildren(this Transform tf) {
        foreach(Transform t in tf) {
            GameObject.Destroy(t.gameObject);
        }
        tf.DetachChildren();
    }
}
