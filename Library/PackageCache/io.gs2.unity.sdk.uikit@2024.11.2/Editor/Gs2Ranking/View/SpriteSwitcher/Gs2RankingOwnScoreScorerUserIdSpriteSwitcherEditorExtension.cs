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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2RankingOwnScoreScorerUserIdSpriteSwitcher))]
    public class Gs2RankingOwnScoreScorerUserIdSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingOwnScoreScorerUserIdSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2RankingOwnScoreContext>() ?? original.GetComponentInParent<Gs2RankingOwnScoreContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnScoreContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingOwnScoreContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2RankingOwnScoreList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingOwnScoreContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Score is auto assign from Gs2RankingOwnScoreList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingOwnScoreContext), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2RankingOwnScoreScorerUserIdSpriteSwitcher.Expression.In || original.expression == Gs2RankingOwnScoreScorerUserIdSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyScorerUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyScorerUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}