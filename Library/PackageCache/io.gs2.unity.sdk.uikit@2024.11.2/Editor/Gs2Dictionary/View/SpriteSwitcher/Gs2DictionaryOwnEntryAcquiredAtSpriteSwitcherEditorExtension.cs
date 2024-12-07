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

using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Editor
{
    [CustomEditor(typeof(Gs2DictionaryOwnEntryAcquiredAtSpriteSwitcher))]
    public class Gs2DictionaryOwnEntryAcquiredAtSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryOwnEntryAcquiredAtSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2DictionaryOwnEntryContext>() ?? original.GetComponentInParent<Gs2DictionaryOwnEntryContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2DictionaryOwnEntryContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2DictionaryOwnEntryContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2DictionaryOwnEntryList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryOwnEntryContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Entry is auto assign from Gs2DictionaryOwnEntryList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryOwnEntryContext), false);
                    EditorGUI.indentLevel++;
                    context.Entry = EditorGUILayout.ObjectField("Entry", context.Entry, typeof(OwnEntry), false) as OwnEntry;
                    if (context.Entry != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Entry?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("EntryModelName", context.Entry?.EntryModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2DictionaryOwnEntryAcquiredAtSpriteSwitcher.Expression.In || original.expression == Gs2DictionaryOwnEntryAcquiredAtSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyAcquiredAts"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyAcquiredAt"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}