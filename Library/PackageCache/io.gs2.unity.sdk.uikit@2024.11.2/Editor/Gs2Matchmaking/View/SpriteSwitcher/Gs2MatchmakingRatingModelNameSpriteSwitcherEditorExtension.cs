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

namespace Gs2.Unity.UiKit.Gs2Matchmaking.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingRatingModelNameSpriteSwitcher))]
    public class Gs2MatchmakingRatingModelNameSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingRatingModelNameSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2MatchmakingRatingModelContext>() ?? original.GetComponentInParent<Gs2MatchmakingRatingModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingRatingModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingRatingModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2MatchmakingRatingModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingRatingModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RatingModel is auto assign from Gs2MatchmakingRatingModelList.", MessageType.Info);
                }
                else if (context.gameObject.GetComponentInParent<Gs2MatchmakingOwnRatingList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingOwnRatingContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RatingModel is auto assign from Gs2MatchmakingOwnRatingList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingRatingModelContext), false);
                    EditorGUI.indentLevel++;
                    context.RatingModel = EditorGUILayout.ObjectField("RatingModel", context.RatingModel, typeof(RatingModel), false) as RatingModel;
                    if (context.RatingModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.RatingModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RatingName", context.RatingModel?.RatingName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MatchmakingRatingModelNameSpriteSwitcher.Expression.In || original.expression == Gs2MatchmakingRatingModelNameSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}