using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Matchmaking.Prefabs
{
    public static class Gs2MatchmakingSeasonGatheringTierValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Matchmaking/Namespace/User/Season/SeasonGathering/Label/TierValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Matchmaking/Prefabs/SeasonGatheringTierValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}