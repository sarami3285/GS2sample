using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Enhance.Prefabs
{
    public static class Gs2EnhanceProgressDeleteProgressButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Enhance/Namespace/User/Progress/Action/DeleteProgress", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Enhance/Prefabs/ProgressDeleteProgressButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}