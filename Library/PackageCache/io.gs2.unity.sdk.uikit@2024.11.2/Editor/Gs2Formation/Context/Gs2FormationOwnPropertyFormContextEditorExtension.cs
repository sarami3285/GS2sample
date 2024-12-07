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
    [CustomEditor(typeof(Gs2FormationOwnPropertyFormContext))]
    public class Gs2FormationOwnPropertyFormContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnPropertyFormContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.PropertyForm == null) {
                var list = original.GetComponentInParent<Gs2FormationOwnPropertyFormList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("PropertyForm is auto assign from Gs2FormationOwnPropertyFormList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2FormationOwnPropertyFormList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2FormationConvertPropertyFormModelToOwnPropertyForm>(true) != null) {
                    EditorGUILayout.HelpBox("PropertyForm is auto assign from Gs2FormationConvertPropertyFormModelToOwnPropertyForm.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2FormationConvertPropertyFormModelToOwnPropertyForm>(true), typeof(Gs2FormationConvertPropertyFormModelToOwnPropertyForm), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnPropertyForm not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_propertyForm"), true);
                }
            }
            else {
                original.PropertyForm = EditorGUILayout.ObjectField("OwnPropertyForm", original.PropertyForm, typeof(OwnPropertyForm), false) as OwnPropertyForm;
                EditorGUI.BeginDisabledGroup(true);
                if (original.PropertyForm != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.PropertyForm?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("PropertyFormModelName", original.PropertyForm?.PropertyFormModelName?.ToString());
                    EditorGUILayout.TextField("PropertyId", original.PropertyForm?.PropertyId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}