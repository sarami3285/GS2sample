using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Schedule.Prefabs
{
    public static class Gs2ScheduleEventAbsoluteBeginValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Schedule/Namespace/User/Event/Label/AbsoluteBeginValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Schedule/Prefabs/EventAbsoluteBeginValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}