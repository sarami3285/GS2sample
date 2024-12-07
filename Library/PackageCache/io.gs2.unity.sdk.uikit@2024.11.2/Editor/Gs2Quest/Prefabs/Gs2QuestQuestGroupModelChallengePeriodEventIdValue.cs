using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Quest.Prefabs
{
    public static class Gs2QuestQuestGroupModelChallengePeriodEventIdValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Quest/Namespace/QuestGroupModel/Label/ChallengePeriodEventIdValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Quest/Prefabs/QuestGroupModelChallengePeriodEventIdValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}