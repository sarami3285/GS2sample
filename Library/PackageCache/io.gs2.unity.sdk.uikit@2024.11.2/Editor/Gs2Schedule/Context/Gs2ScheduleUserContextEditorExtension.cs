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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleUserContext))]
    public class Gs2ScheduleUserContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleUserContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.User == null) {
                EditorGUILayout.HelpBox("User not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("User"), true);
            }
            else {
                original.User = EditorGUILayout.ObjectField("User", original.User, typeof(User), false) as User;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.User?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", original.User?.UserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}