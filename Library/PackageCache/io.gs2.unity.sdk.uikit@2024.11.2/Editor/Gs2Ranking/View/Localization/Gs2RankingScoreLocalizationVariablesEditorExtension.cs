/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Ranking.Localization.Editor
{
    [CustomEditor(typeof(Gs2RankingScoreLocalizationVariables))]
    public class Gs2RankingScoreLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingScoreLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2RankingOwnScoreFetcher>() ?? original.GetComponentInParent<Gs2RankingOwnScoreFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnScoreFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2RankingOwnScoreFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2RankingOwnScoreList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnScoreFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Score is auto assign from Gs2RankingOwnScoreList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2RankingOwnScoreContext>() ?? original.GetComponentInParent<Gs2RankingOwnScoreContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnScoreFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Score = EditorGUILayout.ObjectField("Score", context.Score, typeof(OwnScore), false) as OwnScore;
                    if (context.Score != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Score?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("CategoryName", context.Score?.CategoryName?.ToString());
                        EditorGUILayout.TextField("ScorerUserId", context.Score?.ScorerUserId?.ToString());
                        EditorGUILayout.TextField("UniqueId", context.Score?.UniqueId?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif