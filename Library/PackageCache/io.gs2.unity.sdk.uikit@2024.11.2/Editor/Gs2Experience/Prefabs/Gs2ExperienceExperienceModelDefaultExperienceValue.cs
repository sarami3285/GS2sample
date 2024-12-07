using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Experience.Prefabs
{
    public static class Gs2ExperienceExperienceModelDefaultExperienceValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Experience/Namespace/ExperienceModel/Label/DefaultExperienceValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Experience/Prefabs/ExperienceModelDefaultExperienceValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}