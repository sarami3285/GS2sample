using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Buff.Prefabs
{
    public static class Gs2BuffBuffEntryModelApplyPeriodScheduleEventIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Buff/Namespace/BuffEntryModel/Label/ApplyPeriodScheduleEventIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Buff/Prefabs/BuffEntryModelApplyPeriodScheduleEventIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}