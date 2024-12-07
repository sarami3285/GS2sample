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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience.Editor
{
    [CustomEditor(typeof(Gs2ExperienceOwnStatusEnabler))]
    public class Gs2ExperienceOwnStatusEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceOwnStatusEnabler;

            if (original == null) return;

            serializedObject.Update();

            var fetcher = original.GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? original.GetComponentInParent<Gs2ExperienceOwnStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("StatusFetcher not assigned.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ExperienceOwnStatusFetcher>();
                }
            }
            else {
                DrawDefaultInspector();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}