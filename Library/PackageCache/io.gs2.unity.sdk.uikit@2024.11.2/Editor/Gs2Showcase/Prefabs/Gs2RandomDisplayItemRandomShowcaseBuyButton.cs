using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Showcase.Prefabs
{
    public static class Gs2ShowcaseRandomDisplayItemRandomShowcaseBuyButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Showcase/Namespace/User/RandomShowcase/RandomDisplayItem/Action/RandomShowcaseBuy", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Showcase/Prefabs/RandomDisplayItemRandomShowcaseBuyButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}