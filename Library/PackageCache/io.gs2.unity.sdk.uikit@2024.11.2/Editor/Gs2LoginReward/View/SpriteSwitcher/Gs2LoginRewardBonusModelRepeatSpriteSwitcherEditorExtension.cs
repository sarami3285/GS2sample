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

using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2LoginReward.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2LoginRewardBonusModelRepeatSpriteSwitcher))]
    public class Gs2LoginRewardBonusModelRepeatSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LoginRewardBonusModelRepeatSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2LoginRewardBonusModelContext>() ?? original.GetComponentInParent<Gs2LoginRewardBonusModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2LoginRewardBonusModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2LoginRewardBonusModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2LoginRewardBonusModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LoginRewardBonusModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BonusModel is auto assign from Gs2LoginRewardBonusModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LoginRewardBonusModelContext), false);
                    EditorGUI.indentLevel++;
                    context.BonusModel = EditorGUILayout.ObjectField("BonusModel", context.BonusModel, typeof(BonusModel), false) as BonusModel;
                    if (context.BonusModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BonusModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("BonusModelName", context.BonusModel?.BonusModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2LoginRewardBonusModelRepeatSpriteSwitcher.Expression.In || original.expression == Gs2LoginRewardBonusModelRepeatSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyRepeats"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyRepeat"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}