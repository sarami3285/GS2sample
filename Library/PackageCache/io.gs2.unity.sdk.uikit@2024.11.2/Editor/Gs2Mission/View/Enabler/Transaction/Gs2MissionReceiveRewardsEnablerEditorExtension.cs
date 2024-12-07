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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Enabler.Editor
{
    [CustomEditor(typeof(Gs2MissionReceiveRewardsEnabler))]
    public class Gs2MissionReceiveRewardsEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionReceiveRewardsEnabler;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? original.GetComponentInParent<Gs2MissionMissionTaskModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionMissionTaskModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionMissionTaskModelFetcher>();
                }
            }
            var userDataFetcher = original.GetComponent<Gs2MissionOwnCompleteFetcher>() ?? original.GetComponentInParent<Gs2MissionOwnCompleteFetcher>(true);
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionOwnCompleteFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionOwnCompleteFetcher>();
                }
            }

            DrawDefaultInspector();
        }
    }
}