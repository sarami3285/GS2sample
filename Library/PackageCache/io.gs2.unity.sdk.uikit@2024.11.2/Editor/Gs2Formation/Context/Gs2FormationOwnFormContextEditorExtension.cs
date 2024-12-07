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
    [CustomEditor(typeof(Gs2FormationOwnFormContext))]
    public class Gs2FormationOwnFormContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnFormContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Form == null) {
                var list = original.GetComponentInParent<Gs2FormationOwnFormList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("Form is auto assign from Gs2FormationOwnFormList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2FormationOwnFormList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2FormationConvertFormModelToOwnForm>(true) != null) {
                    EditorGUILayout.HelpBox("Form is auto assign from Gs2FormationConvertFormModelToOwnForm.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2FormationConvertFormModelToOwnForm>(true), typeof(Gs2FormationConvertFormModelToOwnForm), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnForm not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_form"), true);
                }
            }
            else {
                original.Form = EditorGUILayout.ObjectField("OwnForm", original.Form, typeof(OwnForm), false) as OwnForm;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Form != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Form?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("MoldModelName", original.Form?.MoldModelName?.ToString());
                    EditorGUILayout.TextField("Index", original.Form?.Index.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}