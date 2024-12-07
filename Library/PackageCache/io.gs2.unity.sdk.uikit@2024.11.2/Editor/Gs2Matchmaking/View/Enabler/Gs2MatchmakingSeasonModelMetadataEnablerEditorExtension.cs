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

using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Enabler.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingSeasonModelMetadataEnabler))]
    public class Gs2MatchmakingSeasonModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingSeasonModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2MatchmakingSeasonModelContext>() ?? original.GetComponentInParent<Gs2MatchmakingSeasonModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingSeasonModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingSeasonModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2MatchmakingSeasonModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingSeasonModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SeasonModel is auto assign from Gs2MatchmakingSeasonModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingSeasonModelContext), false);
                    EditorGUI.indentLevel++;
                    context.SeasonModel = EditorGUILayout.ObjectField("SeasonModel", context.SeasonModel, typeof(SeasonModel), false) as SeasonModel;
                    if (context.SeasonModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.SeasonModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("SeasonName", context.SeasonModel?.SeasonName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MatchmakingSeasonModelMetadataEnabler.Expression.In || original.expression == Gs2MatchmakingSeasonModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}