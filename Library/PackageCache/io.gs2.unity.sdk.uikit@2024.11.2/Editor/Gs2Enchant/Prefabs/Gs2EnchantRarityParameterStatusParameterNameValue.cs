using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Enchant.Prefabs
{
    public static class Gs2EnchantRarityParameterStatusParameterNameValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Enchant/Namespace/User/RarityParameterStatus/Label/ParameterNameValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Enchant/Prefabs/RarityParameterStatusParameterNameValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}