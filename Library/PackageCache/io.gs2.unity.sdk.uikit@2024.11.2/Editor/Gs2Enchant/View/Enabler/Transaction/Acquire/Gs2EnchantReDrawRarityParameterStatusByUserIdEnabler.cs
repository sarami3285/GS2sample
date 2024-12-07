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
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enchant.Enabler.Editor
{
    [CustomEditor(typeof(Gs2EnchantReDrawRarityParameterStatusByUserIdEnabler))]
    public class Gs2EnchantReDrawRarityParameterStatusByUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantReDrawRarityParameterStatusByUserIdEnabler;

            if (original == null) return;

            var fetcher = original.GetComponent<IAcquireActionsFetcher>() ?? original.GetComponentInParent<IAcquireActionsFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("IAcquireActionsFetcher not found.", MessageType.Error);
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("includeAcquireActions"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("notIncludeAcquireActions"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}