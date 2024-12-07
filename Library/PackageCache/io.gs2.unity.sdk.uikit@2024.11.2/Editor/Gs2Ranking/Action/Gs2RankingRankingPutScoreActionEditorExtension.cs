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
 *
 * deny overwrite
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
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingRankingPutScoreAction))]
    public class Gs2RankingRankingPutScoreActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingRankingPutScoreAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2RankingRankingContext>() ?? original.GetComponentInParent<Gs2RankingRankingContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingRankingContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingRankingContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2RankingRankingList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingRankingContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Ranking is auto assign from Gs2RankingRankingList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingRankingContext), false);
                    EditorGUI.indentLevel++;
                    context.Ranking = EditorGUILayout.ObjectField("Ranking", context.Ranking, typeof(Ranking), false) as Ranking;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Ranking?.NamespaceName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Score"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Metadata"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeScore"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeMetadata"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onPutScoreComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}