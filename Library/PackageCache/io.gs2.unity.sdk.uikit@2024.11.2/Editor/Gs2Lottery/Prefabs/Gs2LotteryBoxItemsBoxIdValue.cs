using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Lottery.Prefabs
{
    public static class Gs2LotteryBoxItemsBoxIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Lottery/Namespace/User/BoxItems/Label/BoxIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Lottery/Prefabs/BoxItemsBoxIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}