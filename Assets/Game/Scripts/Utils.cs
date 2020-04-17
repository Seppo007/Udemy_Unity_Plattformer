using UnityEngine;

public class Utils {
    public static void drawBoxCollider(MonoBehaviour mb, Color color) {

#if UNITY_EDITOR
        // Zeichne Gizmoz nur im Unity Editor
        if (UnityEditor.Selection.activeGameObject != mb.gameObject) {
            BoxCollider boxCollider = mb.GetComponent<BoxCollider>();
            if (boxCollider == null) return;
            Gizmos.color = color;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = mb.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            Gizmos.matrix = oldMatrix;
        }
#endif
    }
}
