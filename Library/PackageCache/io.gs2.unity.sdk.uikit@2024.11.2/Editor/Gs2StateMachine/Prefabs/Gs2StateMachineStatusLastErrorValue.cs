using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2StateMachine.Prefabs
{
    public static class Gs2StateMachineStatusLastErrorValue
    {
        [MenuItem("GameObject/UI/Game Server Services/StateMachine/Namespace/User/Status/Label/LastErrorValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2StateMachine/Prefabs/StatusLastErrorValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}