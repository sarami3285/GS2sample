using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2AdReward.Prefabs
{
    public static class Gs2AdRewardPointPointValue
    {
        [MenuItem("GameObject/UI/Game Server Services/AdReward/Namespace/User/Point/Label/PointValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2AdReward/Prefabs/PointPointValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}