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
    [CustomEditor(typeof(Gs2EnchantBalanceParameterModelContext))]
    public class Gs2EnchantBalanceParameterModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantBalanceParameterModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.BalanceParameterModel == null) {
                var list = original.GetComponentInParent<Gs2EnchantBalanceParameterModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("BalanceParameterModel is auto assign from Gs2EnchantBalanceParameterModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2EnchantBalanceParameterModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("BalanceParameterModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_balanceParameterModel"), true);
                }
            }
            else {
                original.BalanceParameterModel = EditorGUILayout.ObjectField("BalanceParameterModel", original.BalanceParameterModel, typeof(BalanceParameterModel), false) as BalanceParameterModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.BalanceParameterModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.BalanceParameterModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ParameterName", original.BalanceParameterModel?.ParameterName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}