using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Dictionary.Prefabs
{
    public static class Gs2DictionaryEntryEntryIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Dictionary/Namespace/User/Entry/Label/EntryIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Dictionary/Prefabs/EntryEntryIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}