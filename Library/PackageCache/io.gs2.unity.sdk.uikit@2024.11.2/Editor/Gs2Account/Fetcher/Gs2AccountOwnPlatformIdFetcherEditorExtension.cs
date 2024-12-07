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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountOwnPlatformIdFetcher))]
    public class Gs2AccountOwnPlatformIdFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountOwnPlatformIdFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2AccountOwnPlatformIdContext>() ?? original.GetComponentInParent<Gs2AccountOwnPlatformIdContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2AccountOwnPlatformIdContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2AccountOwnPlatformIdContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2AccountOwnPlatformIdList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2AccountOwnPlatformIdContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PlatformId is auto assign from Gs2AccountOwnPlatformIdList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2AccountOwnPlatformIdContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("PlatformId", context.PlatformId, typeof(OwnPlatformId), false);
                    if (context.PlatformId != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.PlatformId?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("Type", context.PlatformId?.Type.ToString());
                        EditorGUILayout.TextField("UserIdentifier", context.PlatformId?.UserIdentifier?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}