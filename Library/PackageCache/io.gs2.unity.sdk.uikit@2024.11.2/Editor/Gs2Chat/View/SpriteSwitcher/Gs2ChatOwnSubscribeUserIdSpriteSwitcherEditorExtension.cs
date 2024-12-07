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

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2ChatOwnSubscribeUserIdSpriteSwitcher))]
    public class Gs2ChatOwnSubscribeUserIdSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatOwnSubscribeUserIdSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2ChatOwnSubscribeContext>() ?? original.GetComponentInParent<Gs2ChatOwnSubscribeContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ChatOwnSubscribeContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ChatOwnSubscribeContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ChatOwnSubscribeList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatOwnSubscribeContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Subscribe is auto assign from Gs2ChatOwnSubscribeList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatOwnSubscribeContext), false);
                    EditorGUI.indentLevel++;
                    context.Subscribe = EditorGUILayout.ObjectField("Subscribe", context.Subscribe, typeof(OwnSubscribe), false) as OwnSubscribe;
                    if (context.Subscribe != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Subscribe?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RoomName", context.Subscribe?.RoomName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ChatOwnSubscribeUserIdSpriteSwitcher.Expression.In || original.expression == Gs2ChatOwnSubscribeUserIdSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}