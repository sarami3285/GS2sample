using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Buff.Prefabs
{
    public static class Gs2BuffBuffEntryModelExpressionValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Buff/Namespace/BuffEntryModel/Label/ExpressionValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Buff/Prefabs/BuffEntryModelExpressionValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}