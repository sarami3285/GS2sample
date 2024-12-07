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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Enabler.Editor
{
    [CustomEditor(typeof(Gs2FriendOwnFriendStatusEnabler))]
    public class Gs2FriendOwnFriendStatusEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendOwnFriendStatusEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2FriendOwnFriendUserContext>() ?? original.GetComponentInParent<Gs2FriendOwnFriendUserContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnFriendUserContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendOwnFriendUserContext>();
                }
            }
            var friendListFetcher = original.GetComponent<Gs2FriendOwnFriendListFetcher>() ?? original.GetComponentInParent<Gs2FriendOwnFriendListFetcher>(true);
            if (friendListFetcher == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnFriendListFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FriendOwnFriendListFetcher>();
                }
            }
            var sendFriendRequestListFetcher = original.GetComponent<Gs2FriendOwnSendFriendRequestListFetcher>() ?? original.GetComponentInParent<Gs2FriendOwnSendFriendRequestListFetcher>(true);
            if (sendFriendRequestListFetcher == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnSendFriendRequestListFetcher not found.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FriendOwnSendFriendRequestListFetcher>();
                }
            }
            var receiveFriendRequestListFetcher = original.GetComponent<Gs2FriendOwnReceiveFriendRequestListFetcher>() ?? original.GetComponentInParent<Gs2FriendOwnReceiveFriendRequestListFetcher>(true);
            if (receiveFriendRequestListFetcher == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnReceiveFriendRequestListFetcher not found.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FriendOwnReceiveFriendRequestListFetcher>();
                }
            }

            DrawDefaultInspector();
        }
    }
}