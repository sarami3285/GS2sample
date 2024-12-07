using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Stamina.Prefabs
{
    public static class Gs2StaminaStaminaModelRecoverIntervalMinutesValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Stamina/Namespace/StaminaModel/Label/RecoverIntervalMinutesValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Stamina/Prefabs/StaminaModelRecoverIntervalMinutesValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}