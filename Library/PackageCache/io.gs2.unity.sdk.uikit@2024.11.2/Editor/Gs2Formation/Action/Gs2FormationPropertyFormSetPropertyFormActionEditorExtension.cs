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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationPropertyFormSetPropertyFormAction))]
    public class Gs2FormationPropertyFormSetPropertyFormActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationPropertyFormSetPropertyFormAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationOwnPropertyFormContext>() ?? original.GetComponentInParent<Gs2FormationOwnPropertyFormContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnPropertyFormContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationOwnPropertyFormContext>();
                }
            }
            else {
                if (context.transform.GetComponentInParent<Gs2FormationOwnPropertyFormList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnPropertyFormContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PropertyForm is auto assign from Gs2FormationPropertyFormList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnPropertyFormContext), false);
                    EditorGUI.indentLevel++;
                    context.PropertyForm = EditorGUILayout.ObjectField("OwnPropertyForm", context.PropertyForm, typeof(OwnPropertyForm), false) as OwnPropertyForm;
                    if (context.PropertyForm != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.PropertyForm?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("PropertyFormModelName", context.PropertyForm?.PropertyFormModelName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.PropertyForm?.PropertyId?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}