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
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Editor
{
    [CustomEditor(typeof(Gs2DictionaryOwnEntryContext))]
    public class Gs2DictionaryOwnEntryContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryOwnEntryContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Entry == null) {
                var list = original.GetComponentInParent<Gs2DictionaryOwnEntryList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("Entry is auto assign from Gs2DictionaryOwnEntryList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2DictionaryOwnEntryList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2DictionaryConvertEntryModelToOwnEntry>(true) != null) {
                    EditorGUILayout.HelpBox("Entry is auto assign from Gs2DictionaryConvertEntryModelToOwnEntry.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2DictionaryConvertEntryModelToOwnEntry>(true), typeof(Gs2DictionaryConvertEntryModelToOwnEntry), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnEntry not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_entry"), true);
                }
            }
            else {
                original.Entry = EditorGUILayout.ObjectField("OwnEntry", original.Entry, typeof(OwnEntry), false) as OwnEntry;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Entry != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Entry?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("EntryModelName", original.Entry?.EntryModelName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}