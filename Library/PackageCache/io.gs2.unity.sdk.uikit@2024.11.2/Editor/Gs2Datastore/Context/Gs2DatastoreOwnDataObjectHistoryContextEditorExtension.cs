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
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Editor
{
    [CustomEditor(typeof(Gs2DatastoreOwnDataObjectHistoryContext))]
    public class Gs2DatastoreOwnDataObjectHistoryContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreOwnDataObjectHistoryContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.DataObjectHistory == null) {
                var list = original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("DataObjectHistory is auto assign from Gs2DatastoreOwnDataObjectHistoryList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2DatastoreOwnDataObjectHistoryList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnDataObjectHistory not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_dataObjectHistory"), true);
                }
            }
            else {
                original.DataObjectHistory = EditorGUILayout.ObjectField("OwnDataObjectHistory", original.DataObjectHistory, typeof(OwnDataObjectHistory), false) as OwnDataObjectHistory;
                EditorGUI.BeginDisabledGroup(true);
                if (original.DataObjectHistory != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.DataObjectHistory?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("DataObjectName", original.DataObjectHistory?.DataObjectName?.ToString());
                    EditorGUILayout.TextField("Generation", original.DataObjectHistory?.Generation?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}