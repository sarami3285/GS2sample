using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Limit.Prefabs
{
    public static class Gs2LimitLimitModelResetDayOfWeekValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Limit/Namespace/LimitModel/Label/ResetDayOfWeekValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Limit/Prefabs/LimitModelResetDayOfWeekValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}