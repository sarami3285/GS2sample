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

using Gs2.Unity.Gs2Grade.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Grade.Context;
using Gs2.Unity.UiKit.Gs2Grade.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Grade.Localization.Editor
{
    [CustomEditor(typeof(Gs2GradeStatusLocalizationVariables))]
    public class Gs2GradeStatusLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2GradeStatusLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2GradeOwnStatusFetcher>() ?? original.GetComponentInParent<Gs2GradeOwnStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2GradeOwnStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2GradeOwnStatusFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2GradeOwnStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2GradeOwnStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2GradeOwnStatusList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2GradeOwnStatusContext>() ?? original.GetComponentInParent<Gs2GradeOwnStatusContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2GradeOwnStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Status = EditorGUILayout.ObjectField("Status", context.Status, typeof(OwnStatus), false) as OwnStatus;
                    if (context.Status != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Status?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("GradeName", context.Status?.GradeName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.Status?.PropertyId?.ToString());
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