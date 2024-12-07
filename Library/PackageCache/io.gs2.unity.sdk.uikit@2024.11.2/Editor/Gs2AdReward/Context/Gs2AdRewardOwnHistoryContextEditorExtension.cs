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

using Gs2.Unity.Gs2AdReward.ScriptableObject;
using Gs2.Unity.UiKit.Gs2AdReward.Context;
using Gs2.Unity.UiKit.Gs2AdReward.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2AdReward.Editor
{
    [CustomEditor(typeof(Gs2AdRewardOwnHistoryContext))]
    public class Gs2AdRewardOwnHistoryContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AdRewardOwnHistoryContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.History == null) {
                EditorGUILayout.HelpBox("OwnHistory not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_history"), true);
            }
            else {
                original.History = EditorGUILayout.ObjectField("OwnHistory", original.History, typeof(OwnHistory), false) as OwnHistory;
                EditorGUI.BeginDisabledGroup(true);
                if (original.History != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.History?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("TransactionId", original.History?.TransactionId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}