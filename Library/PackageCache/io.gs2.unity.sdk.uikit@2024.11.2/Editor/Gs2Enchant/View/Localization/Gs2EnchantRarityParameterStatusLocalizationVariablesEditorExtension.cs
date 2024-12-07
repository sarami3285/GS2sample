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

using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Enchant.Localization.Editor
{
    [CustomEditor(typeof(Gs2EnchantRarityParameterStatusLocalizationVariables))]
    public class Gs2EnchantRarityParameterStatusLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantRarityParameterStatusLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2EnchantOwnRarityParameterStatusFetcher>() ?? original.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnchantOwnRarityParameterStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnchantOwnRarityParameterStatusFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnchantOwnRarityParameterStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RarityParameterStatus is auto assign from Gs2EnchantOwnRarityParameterStatusList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2EnchantOwnRarityParameterStatusContext>() ?? original.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnchantOwnRarityParameterStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    context.RarityParameterStatus = EditorGUILayout.ObjectField("RarityParameterStatus", context.RarityParameterStatus, typeof(OwnRarityParameterStatus), false) as OwnRarityParameterStatus;
                    if (context.RarityParameterStatus != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.RarityParameterStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.RarityParameterStatus?.ParameterName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.RarityParameterStatus?.PropertyId?.ToString());
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