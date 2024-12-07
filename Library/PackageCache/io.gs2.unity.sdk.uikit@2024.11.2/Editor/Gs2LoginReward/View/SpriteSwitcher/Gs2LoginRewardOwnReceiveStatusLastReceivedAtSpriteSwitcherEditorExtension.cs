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

namespace Gs2.Unity.UiKit.Gs2LoginReward.Editor
{
    [CustomEditor(typeof(Gs2LoginRewardOwnReceiveStatusLastReceivedAtSpriteSwitcher))]
    public class Gs2LoginRewardOwnReceiveStatusLastReceivedAtSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LoginRewardOwnReceiveStatusLastReceivedAtSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2LoginRewardOwnReceiveStatusContext>() ?? original.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2LoginRewardOwnReceiveStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2LoginRewardOwnReceiveStatusContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ReceiveStatus is auto assign from Gs2LoginRewardOwnReceiveStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusContext), false);
                    EditorGUI.indentLevel++;
                    context.ReceiveStatus = EditorGUILayout.ObjectField("ReceiveStatus", context.ReceiveStatus, typeof(OwnReceiveStatus), false) as OwnReceiveStatus;
                    if (context.ReceiveStatus != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.ReceiveStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("BonusModelName", context.ReceiveStatus?.BonusModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2LoginRewardOwnReceiveStatusLastReceivedAtSpriteSwitcher.Expression.In || original.expression == Gs2LoginRewardOwnReceiveStatusLastReceivedAtSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyLastReceivedAts"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyLastReceivedAt"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}