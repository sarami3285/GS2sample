using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Stamina.Prefabs
{
    public static class Gs2StaminaStaminaSetMaxValueButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Stamina/Namespace/User/Stamina/Action/SetMaxValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Stamina/Prefabs/StaminaSetMaxValueButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}