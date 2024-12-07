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

using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Version.Context;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Editor
{
    [CustomEditor(typeof(Gs2VersionOwnAcceptVersionContext))]
    public class Gs2VersionOwnAcceptVersionContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionOwnAcceptVersionContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.AcceptVersion == null) {
                var list = original.GetComponentInParent<Gs2VersionOwnAcceptVersionList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("AcceptVersion is auto assign from Gs2VersionOwnAcceptVersionList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2VersionOwnAcceptVersionList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnAcceptVersion not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_acceptVersion"), true);
                }
            }
            else {
                original.AcceptVersion = EditorGUILayout.ObjectField("OwnAcceptVersion", original.AcceptVersion, typeof(OwnAcceptVersion), false) as OwnAcceptVersion;
                EditorGUI.BeginDisabledGroup(true);
                if (original.AcceptVersion != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.AcceptVersion?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("VersionName", original.AcceptVersion?.VersionName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}