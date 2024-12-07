using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2JobQueue.Prefabs
{
    public static class Gs2JobQueueJobScriptIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/JobQueue/Namespace/User/Job/Label/ScriptIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2JobQueue/Prefabs/JobScriptIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}