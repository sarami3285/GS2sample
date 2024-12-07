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

using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Editor
{
    [CustomEditor(typeof(Gs2DatastoreOwnDataObjectHistoryContentLengthSpriteSwitcher))]
    public class Gs2DatastoreOwnDataObjectHistoryContentLengthSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreOwnDataObjectHistoryContentLengthSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2DatastoreOwnDataObjectHistoryContext>() ?? original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2DatastoreOwnDataObjectHistoryContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2DatastoreOwnDataObjectHistoryContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("DataObjectHistory is auto assign from Gs2DatastoreOwnDataObjectHistoryList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryContext), false);
                    EditorGUI.indentLevel++;
                    context.DataObjectHistory = EditorGUILayout.ObjectField("DataObjectHistory", context.DataObjectHistory, typeof(OwnDataObjectHistory), false) as OwnDataObjectHistory;
                    if (context.DataObjectHistory != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.DataObjectHistory?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("DataObjectName", context.DataObjectHistory?.DataObjectName?.ToString());
                        EditorGUILayout.TextField("Generation", context.DataObjectHistory?.Generation?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2DatastoreOwnDataObjectHistoryContentLengthSpriteSwitcher.Expression.In || original.expression == Gs2DatastoreOwnDataObjectHistoryContentLengthSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyContentLengths"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyContentLength"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}