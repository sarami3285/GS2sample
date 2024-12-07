using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Stamina.Prefabs
{
    public static class Gs2StaminaStaminaModelMaxCapacityValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Stamina/Namespace/StaminaModel/Label/MaxCapacityValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Stamina/Prefabs/StaminaModelMaxCapacityValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}