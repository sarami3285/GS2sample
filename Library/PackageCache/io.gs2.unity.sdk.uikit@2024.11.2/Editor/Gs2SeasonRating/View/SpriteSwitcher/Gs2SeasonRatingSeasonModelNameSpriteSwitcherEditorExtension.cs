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

using Gs2.Unity.Gs2SeasonRating.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SeasonRating.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SeasonRating.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2SeasonRatingSeasonModelNameSpriteSwitcher))]
    public class Gs2SeasonRatingSeasonModelNameSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SeasonRatingSeasonModelNameSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2SeasonRatingSeasonModelContext>() ?? original.GetComponentInParent<Gs2SeasonRatingSeasonModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2SeasonRatingSeasonModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2SeasonRatingSeasonModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2SeasonRatingSeasonModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2SeasonRatingSeasonModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SeasonModel is auto assign from Gs2SeasonRatingSeasonModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2SeasonRatingSeasonModelContext), false);
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

            if (original.expression == Gs2SeasonRatingSeasonModelNameSpriteSwitcher.Expression.In || original.expression == Gs2SeasonRatingSeasonModelNameSpriteSwitcher.Expression.NotIn) {
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