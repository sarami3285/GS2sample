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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2LotteryLotteryModelModeSpriteSwitcher))]
    public class Gs2LotteryLotteryModelModeSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryLotteryModelModeSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2LotteryLotteryModelContext>() ?? original.GetComponentInParent<Gs2LotteryLotteryModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2LotteryLotteryModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2LotteryLotteryModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2LotteryLotteryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryLotteryModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("LotteryModel is auto assign from Gs2LotteryLotteryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryLotteryModelContext), false);
                    EditorGUI.indentLevel++;
                    context.LotteryModel = EditorGUILayout.ObjectField("LotteryModel", context.LotteryModel, typeof(LotteryModel), false) as LotteryModel;
                    if (context.LotteryModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.LotteryModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("LotteryName", context.LotteryModel?.LotteryName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2LotteryLotteryModelModeSpriteSwitcher.Expression.In || original.expression == Gs2LotteryLotteryModelModeSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyModes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyMode"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}