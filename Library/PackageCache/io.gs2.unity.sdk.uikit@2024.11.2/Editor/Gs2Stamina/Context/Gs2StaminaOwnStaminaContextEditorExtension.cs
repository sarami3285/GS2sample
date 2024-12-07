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

using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaOwnStaminaContext))]
    public class Gs2StaminaOwnStaminaContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaOwnStaminaContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Stamina == null) {
                var list = original.GetComponentInParent<Gs2StaminaOwnStaminaList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("Stamina is auto assign from Gs2StaminaOwnStaminaList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2StaminaOwnStaminaList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2StaminaConvertStaminaModelToOwnStamina>(true) != null) {
                    EditorGUILayout.HelpBox("Stamina is auto assign from Gs2StaminaConvertStaminaModelToOwnStamina.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2StaminaConvertStaminaModelToOwnStamina>(true), typeof(Gs2StaminaConvertStaminaModelToOwnStamina), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnStamina not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_stamina"), true);
                }
            }
            else {
                original.Stamina = EditorGUILayout.ObjectField("OwnStamina", original.Stamina, typeof(OwnStamina), false) as OwnStamina;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Stamina != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Stamina?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("StaminaName", original.Stamina?.StaminaName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}