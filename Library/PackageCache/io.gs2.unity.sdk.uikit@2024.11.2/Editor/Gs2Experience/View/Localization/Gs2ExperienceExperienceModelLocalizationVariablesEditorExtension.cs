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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Experience.Localization.Editor
{
    [CustomEditor(typeof(Gs2ExperienceExperienceModelLocalizationVariables))]
    public class Gs2ExperienceExperienceModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceExperienceModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ExperienceExperienceModelFetcher>() ?? original.GetComponentInParent<Gs2ExperienceExperienceModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ExperienceExperienceModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ExperienceExperienceModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ExperienceExperienceModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExperienceExperienceModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ExperienceModel is auto assign from Gs2ExperienceExperienceModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ExperienceExperienceModelContext>() ?? original.GetComponentInParent<Gs2ExperienceExperienceModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExperienceExperienceModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.ExperienceModel = EditorGUILayout.ObjectField("ExperienceModel", context.ExperienceModel, typeof(ExperienceModel), false) as ExperienceModel;
                    if (context.ExperienceModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.ExperienceModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ExperienceName", context.ExperienceModel?.ExperienceName?.ToString());
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