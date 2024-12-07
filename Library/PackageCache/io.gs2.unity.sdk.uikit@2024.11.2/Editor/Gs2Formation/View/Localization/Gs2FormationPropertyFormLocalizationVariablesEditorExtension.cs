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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Formation.Localization.Editor
{
    [CustomEditor(typeof(Gs2FormationPropertyFormLocalizationVariables))]
    public class Gs2FormationPropertyFormLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationPropertyFormLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FormationOwnPropertyFormFetcher>() ?? original.GetComponentInParent<Gs2FormationOwnPropertyFormFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnPropertyFormFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationOwnPropertyFormFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2FormationOwnPropertyFormList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnPropertyFormFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PropertyForm is auto assign from Gs2FormationOwnPropertyFormList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2FormationOwnPropertyFormContext>() ?? original.GetComponentInParent<Gs2FormationOwnPropertyFormContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnPropertyFormFetcher), false);
                    EditorGUI.indentLevel++;
                    context.PropertyForm = EditorGUILayout.ObjectField("PropertyForm", context.PropertyForm, typeof(OwnPropertyForm), false) as OwnPropertyForm;
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif