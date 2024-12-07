using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Grade.Prefabs
{
    public static class Gs2GradeStatusApplyRankCapButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Grade/Namespace/User/Status/Action/ApplyRankCap", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Grade/Prefabs/StatusApplyRankCapButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}