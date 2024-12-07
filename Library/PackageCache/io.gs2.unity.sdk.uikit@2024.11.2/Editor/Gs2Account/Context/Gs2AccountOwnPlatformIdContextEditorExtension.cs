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
    [CustomEditor(typeof(Gs2AccountOwnPlatformIdContext))]
    public class Gs2AccountOwnPlatformIdContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountOwnPlatformIdContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.PlatformId == null) {
                var list = original.GetComponentInParent<Gs2AccountOwnPlatformIdList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("PlatformId is auto assign from Gs2AccountOwnPlatformIdList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2AccountOwnPlatformIdList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnPlatformId not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_platformId"), true);
                }
            }
            else {
                original.PlatformId = EditorGUILayout.ObjectField("OwnPlatformId", original.PlatformId, typeof(OwnPlatformId), false) as OwnPlatformId;
                EditorGUI.BeginDisabledGroup(true);
                if (original.PlatformId != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.PlatformId?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("Type", original.PlatformId?.Type.ToString());
                    EditorGUILayout.TextField("UserIdentifier", original.PlatformId?.UserIdentifier?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}