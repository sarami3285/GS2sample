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

using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enchant.Editor
{
    [CustomEditor(typeof(Gs2EnchantOwnRarityParameterStatusContext))]
    public class Gs2EnchantOwnRarityParameterStatusContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantOwnRarityParameterStatusContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.RarityParameterStatus == null) {
                var list = original.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("RarityParameterStatus is auto assign from Gs2EnchantOwnRarityParameterStatusList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2EnchantOwnRarityParameterStatusList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnRarityParameterStatus not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_rarityParameterStatus"), true);
                }
            }
            else {
                original.RarityParameterStatus = EditorGUILayout.ObjectField("OwnRarityParameterStatus", original.RarityParameterStatus, typeof(OwnRarityParameterStatus), false) as OwnRarityParameterStatus;
                EditorGUI.BeginDisabledGroup(true);
                if (original.RarityParameterStatus != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.RarityParameterStatus?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ParameterName", original.RarityParameterStatus?.ParameterName?.ToString());
                    EditorGUILayout.TextField("PropertyId", original.RarityParameterStatus?.PropertyId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}