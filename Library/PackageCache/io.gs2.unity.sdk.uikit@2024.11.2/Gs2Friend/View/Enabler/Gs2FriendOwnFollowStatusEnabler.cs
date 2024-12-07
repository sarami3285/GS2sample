// /*
//  * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
//  * Reserved.
//  *
//  * Licensed under the Apache License, Version 2.0 (the "License").
//  * You may not use this file except in compliance with the License.
//  * A copy of the License is located at
//  *
//  *  http://www.apache.org/licenses/LICENSE-2.0
//  *
//  * or in the "license" file accompanying this file. This file is distributed
//  * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
//  * express or implied. See the License for the specific language governing
//  * permissions and limitations under the License.
//  */
// // ReSharper disable UnusedAutoPropertyAccessor.Global
// // ReSharper disable CheckNamespace
// // ReSharper disable RedundantNameQualifier
// // ReSharper disable RedundantAssignment
// // ReSharper disable NotAccessedVariable
// // ReSharper disable RedundantUsingDirective
// // ReSharper disable Unity.NoNullPropagation
// // ReSharper disable InconsistentNaming
//
// #pragma warning disable CS0472
//
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Gs2.Gs2Friend.Request;
// using Gs2.Unity.UiKit.Core;
// using Gs2.Unity.UiKit.Gs2Core.Fetcher;
// using Gs2.Unity.UiKit.Gs2Friend.Context;
// using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
// using Gs2.Util.LitJson;
// using UnityEngine;
// using UnityEngine.Events;
//
// namespace Gs2.Unity.UiKit.Gs2Friend
// {
//     /// <summary>
//     /// Main
//     /// </summary>
//
// 	[AddComponentMenu("GS2 UIKit/Friend/FollowUser/View/Enabler/Gs2FriendFollowStatusEnabler")]
//     public partial class Gs2FriendOwnFollowStatusEnabler : MonoBehaviour
//     {
//         private void OnFetched()
//         {
//             if (!this._fetcher.FollowUsers.Select(v => v.UserId).Contains(this._context.FollowUser.TargetUserId)) {
//                 this.target.SetActive(this.followed);
//             }
//             else {
//                 this.target.SetActive(this.unrelated);
//             }
//         }
//     }
//
//     /// <summary>
//     /// Dependent components
//     /// </summary>
//
//     public partial class Gs2FriendOwnFollowStatusEnabler
//     {
//         private Gs2FriendOwnFollowUserContext _context;
//         private Gs2FriendOwnFollowUserListFetcher _fetcher;
//
//         public void Awake()
//         {
//             this._context = GetComponent<Gs2FriendOwnFollowUserContext>() ?? GetComponentInParent<Gs2FriendOwnFollowUserContext>();
//             this._fetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>();
//
//             if (this._context == null) {
//                 Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFollowUserContext.");
//                 enabled = false;
//             }
//             if (this._fetcher == null) {
//                 Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFollowUserListFetcher.");
//                 enabled = false;
//             }
//             if (this.target == null) {
//                 Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
//                 enabled = false;
//             }
//             if (this._fetcher.Fetched) {
//                 OnFetched();
//             }
//         }
//
//         public bool HasError()
//         {
//             this._context = GetComponent<Gs2FriendOwnFollowUserContext>() ?? GetComponentInParent<Gs2FriendOwnFollowUserContext>(true);
//             if (this._context == null) {
//                 return true;
//             }
//             this._fetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>(true);
//             if (this._fetcher == null) {
//                 return true;
//             }
//             if (this.target == null) {
//                 return true;
//             }
//             return false;
//         }
//
//         private UnityAction _onFetched;
//
//         public void OnEnable()
//         {
//             this._onFetched = () =>
//             {
//                 OnFetched();
//             };
//             this._fetcher.OnFetched.AddListener(this._onFetched);
//             this._context.OnUpdate.AddListener(this._onFetched);
//
//             if (this._fetcher.Fetched) {
//                 OnFetched();
//             }
//         }
//
//         public void OnDisable()
//         {
//             if (this._onFetched != null) {
//                 this._fetcher.OnFetched.RemoveListener(this._onFetched);
//                 this._context.OnUpdate.RemoveListener(this._onFetched);
//                 this._onFetched = null;
//             }
//         }
//     }
//
//     /// <summary>
//     /// Public properties
//     /// </summary>
//
//     public partial class Gs2FriendOwnFollowStatusEnabler
//     {
//
//     }
//
//     /// <summary>
//     /// Parameters for Inspector
//     /// </summary>
//
//     public partial class Gs2FriendOwnFollowStatusEnabler
//     {
//         public bool followed;
//         public bool unrelated;
//         public GameObject target;
//     }
//
//     /// <summary>
//     /// Event handlers
//     /// </summary>
//     public partial class Gs2FriendOwnFollowStatusEnabler
//     {
//
//     }
// }