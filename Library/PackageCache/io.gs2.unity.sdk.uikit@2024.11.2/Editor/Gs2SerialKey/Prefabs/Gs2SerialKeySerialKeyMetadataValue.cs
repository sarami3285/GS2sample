using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2SerialKey.Prefabs
{
    public static class Gs2SerialKeySerialKeyMetadataValue
    {
        [MenuItem("GameObject/UI/Game Server Services/SerialKey/Namespace/User/SerialKey/Label/MetadataValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2SerialKey/Prefabs/SerialKeyMetadataValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}