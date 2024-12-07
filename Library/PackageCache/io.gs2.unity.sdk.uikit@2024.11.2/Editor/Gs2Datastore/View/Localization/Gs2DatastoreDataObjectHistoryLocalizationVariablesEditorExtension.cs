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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Datastore.Localization.Editor
{
    [CustomEditor(typeof(Gs2DatastoreDataObjectHistoryLocalizationVariables))]
    public class Gs2DatastoreDataObjectHistoryLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreDataObjectHistoryLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2DatastoreOwnDataObjectHistoryFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("DataObjectHistory is auto assign from Gs2DatastoreOwnDataObjectHistoryList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2DatastoreOwnDataObjectHistoryContext>() ?? original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryFetcher), false);
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif