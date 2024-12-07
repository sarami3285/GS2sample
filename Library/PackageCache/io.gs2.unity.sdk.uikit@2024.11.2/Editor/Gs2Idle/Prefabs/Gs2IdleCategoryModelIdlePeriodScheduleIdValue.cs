using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Idle.Prefabs
{
    public static class Gs2IdleCategoryModelIdlePeriodScheduleIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Idle/Namespace/CategoryModel/Label/IdlePeriodScheduleIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Idle/Prefabs/CategoryModelIdlePeriodScheduleIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}